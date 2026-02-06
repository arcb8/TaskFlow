using TaskFlow.Application.Contracts;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Interfaces;

public interface ITaskService
{
    public Task<List<TaskEntity>> GetAllAsync();
    public Task<TaskEntity> GetByIdAsync(Guid id);
    public Task<TaskEntity> CreateAsync(CreateTaskEntityRequest request);
    public Task<TaskEntity> UpdateAsync(Guid id, UpdateTaskEntityRequest request);
    public Task<bool> DeleteAsync(Guid id);
    public Task<TaskEntity> ChangeStatusAsync(Guid id, ChangeStatusRequest request);
    public Task<TaskEntity> AssignAsync(Guid id, AssignRequest request);
}