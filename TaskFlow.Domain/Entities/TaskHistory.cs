namespace TaskFlow.Domain.Entities;

public class TaskHistory
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public int OldStatus { get; set; }
    public int NewStatus { get; set; }
    public DateTime ChangedAt { get; set; }
    public string Comment { get; set; }
}