using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskOrganizer.API.Models
{
    
    public class Comment
    {
        [Key]
        public required Guid Id { get; set; }

        [Required]
        public required Guid TaskId { get; set; }

        [Required]
        public required string CreatedByUid { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Text { get; set; }

        [Required]
        public required DateTime CreatedDate { get; init; }

        [Required]
        public required DateTime UpdatedDate { get; set; }
    }
}
