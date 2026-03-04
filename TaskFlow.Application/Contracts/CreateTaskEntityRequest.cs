using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Application.Contracts;

public class CreateTaskEntityRequest
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "Descriptions is required")]
    public string Descriptions { get; set; }
    
    [Required(ErrorMessage = "Assignee is required")]
    public string Assignee { get; set; }
    
    [Required(ErrorMessage = "DueDate is required")]
    [DataType(DataType.DateTime)]
    public DateTime DueDate { get; set; }
}