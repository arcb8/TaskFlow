using TaskFlow.Domain.Exceptions;
using MyTaskStatus = TaskFlow.Domain.Enums.TaskStatus;

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
    
    // Конструктор
    public TaskEntity(string title, string descriptions, string assignee, DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(title)) 
            throw new DomainException("Title cannot be empty");
        if (string.IsNullOrWhiteSpace(assignee)) 
            throw new DomainException("Assignee cannot be empty");

        Id = Guid.NewGuid();
        Title = title;
        Descriptions = descriptions;
        Assignee = assignee;
        DueDate = dueDate;
        Status = MyTaskStatus.New;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Методы для бизнес-логики
    public void ChangeStatus(MyTaskStatus newStatus)
    {
        if (Status == newStatus)
            throw new DomainException("Status is already the same");

        if (Status == MyTaskStatus.Done || Status == MyTaskStatus.Cancelled)
            throw new DomainException("Cannot change status of completed or cancelled task");

        Status = newStatus;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Assign(string assignee)
    {
        if (string.IsNullOrWhiteSpace(assignee))
            throw new DomainException("Assignee cannot be empty");
        if (assignee == Assignee)
            throw new DomainException("Task is already assigned to this person");
        if (Status == MyTaskStatus.Done || Status == MyTaskStatus.Cancelled)
            throw new DomainException("Cannot assign a task that is completed or cancelled");

        Assignee = assignee;
        UpdatedAt = DateTime.UtcNow;
    }
}