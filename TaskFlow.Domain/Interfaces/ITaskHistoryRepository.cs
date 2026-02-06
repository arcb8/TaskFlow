using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces;

public interface ITaskHistoryRepository
{
    public Task<List<TaskHistory>> GetAllByTaskIdAsync(Guid taskId);
    public Task<TaskHistory?> GetByIdAsync(Guid id);
    public Task<TaskHistory> CreateAsync(TaskHistory history);
    public Task<bool> DeleteAsync(Guid id);
}