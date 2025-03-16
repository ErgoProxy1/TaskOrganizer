using Google.Cloud.Firestore;

namespace TaskOrganizer.API.Models
{
  public class TaskModel
  {
    public required Guid Id { get; set; }
    
    public required string Title { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public string AssignedToUid { get; set; } = string.Empty;
    
    public required string CreatedByUid { get; init; }
    
    public string Status { get; set; } = string.Empty;
    
    public string Priority { get; set; } = string.Empty;
    
    public DateTime? DueDate { get; set; } = null;
    
    public DateTime? Reminder { get; set; } = null;
    
    public required DateTime CreatedDate { get; init; }
    
    public required DateTime UpdatedDate { get; set; }
  }
}
