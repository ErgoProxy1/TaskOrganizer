using Google.Cloud.Firestore;

namespace TaskOrganizer.API.Models
{
    
    public class CommentModel
    {

        public required Guid Id { get; set; }
        public required Guid TaskId { get; set; }
        
        public required string CreatedByUid { get; set; }
        
        public required string Text { get; set; }
        
        public required DateTime CreatedDate { get; init; }
        
        public required DateTime UpdatedDate { get; set; }
    }
}
