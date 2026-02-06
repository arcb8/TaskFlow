using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Contracts;

public class UpdateTaskEntityRequest
{
    public string Title { get; set; }
    public string Descriptions { get; set; }
    public string Assignee { get; set; }
    public MyTaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }
}