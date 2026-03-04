using System.ComponentModel.DataAnnotations;
using TaskFlow.Domain.Enums;
using TaskStatus = TaskFlow.Domain.Enums.TaskStatus;

namespace TaskFlow.Application.Contracts;

public class UpdateTaskEntityRequest
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Descriptions is required")]
    public string Descriptions { get; set; }

    [Required(ErrorMessage = "Assignee is required")]
    public string Assignee { get; set; }

    [Required(ErrorMessage = "Status is required")]
    [EnumDataType(typeof(TaskStatus), ErrorMessage = "Invalid status")]
    public TaskStatus Status { get; set; }

    [Required(ErrorMessage = "DueDate is required")]
    [DataType(DataType.DateTime)]
    public DateTime DueDate { get; set; }
}