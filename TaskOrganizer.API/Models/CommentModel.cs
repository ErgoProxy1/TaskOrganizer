using Google.Cloud.Firestore;

namespace TaskOrganizer.API.Models
{
  [FirestoreData]
  public class CommentModel
  {
    public string Id { get; set; } = string.Empty;
    public string TaskId { get; set; } = string.Empty;
    [FirestoreProperty]
    public required string CreatedByUid { get; set; }
    [FirestoreProperty]
    public required string Text { get; set; }
    [FirestoreProperty]
    public required DateTime CreatedDate { get; init; }
    [FirestoreProperty]
    public required DateTime UpdatedDate { get; set; }
  }
}
