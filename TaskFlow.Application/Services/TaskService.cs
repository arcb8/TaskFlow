using TaskFlow.Application.Contracts;
using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Services;

public class TaskService : ITaskService
{
    private ITaskRepository _taskRepository;
    private ITaskService _taskServiceImplementation;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    // Получить все задачи
    public async Task<List<TaskEntity>> GetAllAsync()
    {
        return await _taskRepository.GetAllAsync();
    }

    // Получить задачу по Id
    public async Task<TaskEntity> GetByIdAsync(Guid id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }

    
    // Сервис принимает DTO, а в репозиторий передает доменную модель (маппит DTO в доменную модель)
    // Создать новую задачу
    public async Task<TaskEntity> CreateAsync(CreateTaskEntityRequest request) // нужнро создать доменную модель
    {
        var task = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Descriptions = request.Descriptions,
            Assignee = request.Assignee,
            DueDate = request.DueDate,

            Status = MyTaskStatus.New,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdTask = await _taskRepository.CreateAsync(task);
        return createdTask;
    }

    // Обновить существующую задачу
    public async Task<TaskEntity> UpdateAsync(Guid id, UpdateTaskEntityRequest request)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null)
            throw new Exception("Task not found"); // можно потом сделать NotFoundException

        // Обновляем поля
        existingTask.Title = request.Title;
        existingTask.Descriptions = request.Descriptions;
        existingTask.Assignee = request.Assignee;
        existingTask.Status = request.Status;
        existingTask.DueDate = request.DueDate;
        existingTask.UpdatedAt = DateTime.UtcNow;

        var task = await _taskRepository.UpdateAsync(existingTask);
        return task;
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
            throw new Exception("Task not found");

        var oldStatus = task.Status;
        task.Status = request.Status;
        task.UpdatedAt = DateTime.UtcNow;

        var updatedTask = await _taskRepository.UpdateAsync(task);
        
        return updatedTask;
    }

    // Назначить исполнителя
    public async Task<TaskEntity> AssignAsync(Guid id, AssignRequest request)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new Exception("Task not found");

        var oldAssignee = task.Assignee;
        task.Assignee = request.Assignee;
        task.UpdatedAt = DateTime.UtcNow;

        var updatedTask = await _taskRepository.UpdateAsync(task);

        return updatedTask;
    }
}