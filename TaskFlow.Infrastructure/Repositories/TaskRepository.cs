using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Infrastructure.Data;

namespace TaskFlow.Infrastructure;

public class TaskRepository : ITaskRepository
{
    private AppDbContext _dbContext;

    public TaskRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TaskEntity>> GetAllAsync()
    {
        // Возвращаем все задачи из базы
        return await _dbContext.Tasks.ToListAsync();
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id)
    {
        // Находим задачу по Id
        return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskEntity> CreateAsync(TaskEntity task)
    {
        // Добавляем задачу в DbContext
        await _dbContext.Tasks.AddAsync(task);
        await _dbContext.SaveChangesAsync(); // сохраняем изменения в базе
        return task;
    }

    public async Task<TaskEntity> UpdateAsync(TaskEntity task)
    {
        // EF Core отслеживает изменения
        _dbContext.Tasks.Update(task);
        await _dbContext.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => Equals(t.Id, id));
        if (task == null)
            return false;

        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}