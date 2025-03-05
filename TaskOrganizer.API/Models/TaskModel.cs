using Google.Cloud.Firestore;

namespace TaskOrganizer.API.Models
{
  [FirestoreData]
  public class TaskModel
  {
    public required string TaskId { get; init; }
    [FirestoreProperty]
    public required string Title { get; set; }
    [FirestoreProperty]
    public string Description { get; set; } = string.Empty;
    [FirestoreProperty]
    public string AssignedToUid { get; set; } = string.Empty;
    [FirestoreProperty]
    public required string CreatedByUid { get; init; }
    [FirestoreProperty]
    public string Status { get; set; } = string.Empty;
    [FirestoreProperty]
    public string Priority { get; set; } = string.Empty;
    [FirestoreProperty]
    public DateTime? DueDate { get; set; } = null;
    [FirestoreProperty]
    public DateTime? Reminder { get; set; } = null;
    [FirestoreProperty]
    public required DateTime CreatedDate { get; init; }
    [FirestoreProperty]
    public required DateTime UpdatedDate { get; set; }
    // public List<CommentModel> Comments { get; set; }
    // public List<?> Attachments { get; set; }
    // public List<?> Tags { get; set; }
    // public string ActivityLog { get; set; }
  }
}
