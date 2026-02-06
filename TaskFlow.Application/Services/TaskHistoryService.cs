using TaskFlow.Application.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Services;

public class TaskHistoryService : ITaskHistoryService
{
    private ITaskHistoryRepository _taskHistoryRepository;

    public TaskHistoryService(ITaskHistoryRepository taskHistoryRepository)
    {
        _taskHistoryRepository = taskHistoryRepository;
    }
    
    public async Task<List<TaskHistory>> GetAllByTaskIdAsync(Guid taskId)
    {
        // Просто вызываем метод репозитория
        var histories = await _taskHistoryRepository.GetAllByTaskIdAsync(taskId);
        return histories;
    }

    public async Task<TaskHistory?> GetByIdAsync(Guid id)
    {
        var history = await _taskHistoryRepository.GetByIdAsync(id);
        return history; // вернёт null, если записи нет
    }

    public async Task<TaskHistory> CreateAsync(TaskHistory history)
    {
        // Можно добавить здесь автоматическую установку Id и времени, если репозиторий этого не делает
        history.Id = Guid.NewGuid();
        history.ChangedAt = DateTime.UtcNow;

        var createdHistory = await _taskHistoryRepository.CreateAsync(history);
        return createdHistory;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _taskHistoryRepository.DeleteAsync(id);
        return result; // true если удалено, false если записи не было
    }
}