using TaskFlow.Domain.Enums;

namespace TaskFlow.Domain.Entities;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Descriptions { get; set; }
    public string Assignee { get; set; }
    public MyTaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}