using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskOrganizer.API.Models
{
    public class UserProject
    {
        [Key]
        [Column(Order = 0)] 
        public required string UserId { get; set; }

        [ForeignKey(nameof(UserId))] 
        public required User User { get; set; }

        [Key]
        [Column(Order = 1)] 
        public required Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public required Project Project { get; set; }

        public string Role { get; set; } = "user";
    }
}
