namespace TaskFlow.Domain.Entities;

public class TaskHistory
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public TaskStatus OldStatus { get; set; }
    public TaskStatus NewStatus { get; set; }
    public DateTime ChangedAt { get; set; }
    public string Comment { get; set; }
}