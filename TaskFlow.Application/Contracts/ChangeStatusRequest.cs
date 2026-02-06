using TaskFlow.Domain.Enums;

namespace TaskFlow.Application.Contracts;

public class ChangeStatusRequest
{
    public MyTaskStatus Status { get; set; }
}