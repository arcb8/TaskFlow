using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

public DbSet<TaskEntity> Tasks { get; set; }
public DbSet<TaskHistory> TaskHistories { get; set; }
}