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

    // Создать новую задачу
    public async Task<TaskEntity> CreateAsync(TaskEntity task)
    {
        task.Id = Guid.NewGuid();            // генерируем уникальный Id
        task.CreatedAt = DateTime.UtcNow;    // дата создания
        task.UpdatedAt = DateTime.UtcNow;    // дата последнего изменения
        task.Status = MyTaskStatus.New;        // по умолчанию новый статус

        var createdTask = await _taskRepository.CreateAsync(task);
        return createdTask;
    }

    // Обновить существующую задачу
    public async Task<TaskEntity> UpdateAsync(Guid id, TaskEntity updatedTask)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null)
            throw new Exception("Task not found"); // можно потом сделать NotFoundException

        // Обновляем поля
        existingTask.Title = updatedTask.Title;
        existingTask.Descriptions = updatedTask.Descriptions;
        existingTask.Assignee = updatedTask.Assignee;
        existingTask.Status = updatedTask.Status;
        existingTask.DueDate = updatedTask.DueDate;
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
    public async Task<TaskEntity> ChangeStatusAsync(Guid id, MyTaskStatus newStatus)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new Exception("Task not found");

        var oldStatus = task.Status;
        task.Status = newStatus;
        task.UpdatedAt = DateTime.UtcNow;

        var updatedTask = await _taskRepository.UpdateAsync(task);
        
        return updatedTask;
    }

    // Назначить исполнителя
    public async Task<TaskEntity> AssignAsync(Guid id, string assignee)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new Exception("Task not found");

        var oldAssignee = task.Assignee;
        task.Assignee = assignee;
        task.UpdatedAt = DateTime.UtcNow;

        var updatedTask = await _taskRepository.UpdateAsync(task);

        return updatedTask;
    }
}