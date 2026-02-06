using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Interfaces;

public interface ITaskService
{
    public Task<List<TaskEntity>> GetAllAsync();
    public Task<TaskEntity> GetByIdAsync(Guid id);
    public Task<TaskEntity> CreateAsync(TaskEntity task);
    public Task<TaskEntity> UpdateAsync(Guid id, TaskEntity task);
    public Task<bool> DeleteAsync(Guid id);
    public Task<TaskEntity> ChangeStatusAsync(Guid id, MyTaskStatus newStatus);
    public Task<TaskEntity> AssignAsync(Guid id, string assignee);
}