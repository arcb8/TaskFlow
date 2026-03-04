using TaskFlow.Domain.Enums;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.Contracts;

public class ChangeStatusRequest
{
    public TaskStatus Status { get; set; }
}