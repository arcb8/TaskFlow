using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure;

public class TaskHistoryRepository : ITaskHistoryRepository
{
    private AppDbContext _dbContext;

    public TaskHistoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // Получить все записи истории по TaskId
    public async Task<List<TaskHistory>> GetAllByTaskIdAsync(Guid taskId)
    {
        // AsNoTracking ускоряет чтение, EF не отслеживает объекты
        return await _dbContext.TaskHistories
            .AsNoTracking()
            .Where(h => h.TaskId == taskId)
            .ToListAsync();
    }

    // Получить запись истории по Id
    public async Task<TaskHistory?> GetByIdAsync(Guid id)
    {
        return await _dbContext.TaskHistories
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    // Создать новую запись истории
    public async Task<TaskHistory> CreateAsync(TaskHistory history)
    {
        // Генерируем уникальный Id и ставим время изменения
        history.Id = Guid.NewGuid();
        history.ChangedAt = DateTime.UtcNow;

        await _dbContext.TaskHistories.AddAsync(history);
        await _dbContext.SaveChangesAsync(); // сохраняем в базу

        return history;
    }

    // Удалить запись истории по Id
    public async Task<bool> DeleteAsync(Guid id)
    {
        var history = await _dbContext.TaskHistories
            .FirstOrDefaultAsync(h => h.Id == id);
        if (history == null)
            return false; // если записи нет, возвращаем false

        _dbContext.TaskHistories.Remove(history);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}