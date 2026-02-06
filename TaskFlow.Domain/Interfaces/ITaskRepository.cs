using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;

public interface ITaskRepository
{
    public Task<List<TaskEntity>> GetAllAsync();
    public Task<TaskEntity> GetByIdAsync(Guid id);
    public Task<TaskEntity> CreateAsync(TaskEntity task);
    public Task<TaskEntity> UpdateAsync(TaskEntity task);
    public Task<bool> DeleteAsync(Guid id);
}
