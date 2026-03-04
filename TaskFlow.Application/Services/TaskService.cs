using TaskFlow.Application.Contracts;
using TaskFlow.Application.Exceptions;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Domain.Entities;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.Services;

public class TaskService : ITaskService
{
    private ITaskRepository _taskRepository;
    private ITaskHistoryService _taskHistoryService;
    
    public TaskService(ITaskRepository taskRepository, ITaskHistoryService taskHistoryService)
    {
        _taskRepository = taskRepository;
        _taskHistoryService = taskHistoryService;
    }
    
    // Получить все задачи
    public async Task<List<TaskEntity>> GetAllAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    // Получить задачу по Id
    public async Task<TaskEntity> GetByIdAsync(Guid id)
    {
        var task = await _taskRepository.GetByIdAsync(id);

        if (task == null)
            throw new NotFoundException($"Task with id {id} not found");

        return task;
    }

    
    // Сервис принимает DTO, а в репозиторий передает доменную модель (маппит DTO в доменную модель)
    // Создать новую задачу
    public async Task<TaskEntity> CreateAsync(CreateTaskEntityRequest request) // нужно создать доменную модель
    {
        // создаём живую сущность через конструктор
        var task = new TaskEntity(
            title: request.Title,
            descriptions: request.Descriptions,
            assignee: request.Assignee,
            dueDate: request.DueDate
        );

        var createdTask = await _taskRepository.CreateAsync(task);
        return createdTask;
    }

    // Обновить существующую задачу
    public async Task<TaskEntity> UpdateAsync(Guid id, UpdateTaskEntityRequest request)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null)
            throw new NotFoundException($"Task with id {id} not found"); // NotFoundException здесь

        // Допустим, бизнес-правило: нельзя менять задачу, если она Cancelled
        if (existingTask.Status == TaskStatus.Cancelled)
            throw new BusinessRuleException("Cannot update a cancelled task"); // BusinessRuleException

        existingTask.Title = request.Title;
        existingTask.Descriptions = request.Descriptions;
        existingTask.Assignee = request.Assignee;
        existingTask.Status = request.Status;
        existingTask.DueDate = request.DueDate;
        existingTask.UpdatedAt = DateTime.UtcNow;

        return await _taskRepository.UpdateAsync(existingTask);
    }

    // Удалить задачу
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _taskRepository.DeleteAsync(id);
    }

    // Сменить статус задачи
    public async Task<TaskEntity> ChangeStatusAsync(Guid id, ChangeStatusRequest request)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new NotFoundException($"Task with id {id} not found");

        var oldStatus = task.Status;
        var newStatus = request.Status;
        

        if (oldStatus == newStatus)
            throw new BusinessRuleException("Status is already the same"); // опционально, бизнес-правило

        // Обновляем задачу
        task.Status = newStatus;
        task.UpdatedAt = DateTime.UtcNow;
        var updatedTask = await _taskRepository.UpdateAsync(task);

        var history = new TaskHistory
        {
            TaskId = task.Id,
            OldStatus = (int)oldStatus,
            NewStatus = (int)newStatus,
            ChangedAt = DateTime.UtcNow,
            Comment = $"Status changed from {oldStatus} to {newStatus}"
        };

        await _taskHistoryService.CreateAsync(history);

        return updatedTask;
    }

    // Назначить исполнителя
    public async Task<TaskEntity> AssignAsync(Guid id, AssignRequest request)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new NotFoundException($"Task with id {id} not found");

        // Проверка 1: assignee не null
        if (string.IsNullOrWhiteSpace(request.Assignee))
            throw new ValidationException(new List<string> { "Assignee cannot be empty" });

        // Проверка 2: нельзя назначить того же человека
        if (task.Assignee == request.Assignee)
            throw new BusinessRuleException("Task is already assigned to this person");

        // Проверка 3: нельзя назначить задачу со статусом Done или Cancelled
        if (task.Status == TaskStatus.Done || task.Status == TaskStatus.Cancelled)
            throw new BusinessRuleException("Cannot assign a task that is completed or cancelled");

        // Всё ок, обновляем
        var oldAssignee = task.Assignee;
        task.Assignee = request.Assignee;
        task.UpdatedAt = DateTime.UtcNow;

        var updatedTask = await _taskRepository.UpdateAsync(task);

        return updatedTask;
    }
}