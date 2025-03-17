using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace TaskOrganizer.API.Models
{
    public class Task
    {
        [Key]
        public required Guid Id { get; set; }
        [Required]
        [MaxLength(60)]
        public required string Title { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;
        public string AssignedToUid { get; set; } = string.Empty;
        [Required]
        public required string CreatedByUid { get; init; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; } = null;
        public DateTime? Reminder { get; set; } = null;
        [Required]
        public required DateTime CreatedDate { get; init; }
        [Required]
        public required DateTime UpdatedDate { get; set; }
    }
}
