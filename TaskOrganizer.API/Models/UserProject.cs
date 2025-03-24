using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskOrganizer.API.Models
{
    public class UserProject
    {
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; } = "";

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public User User { get; set; } = null!;

        [Key]
        [Column(Order = 1)] 
        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        [JsonIgnore]
        public Project Project { get; set; } = null!;

        public string Role { get; set; } = "user";
    }
}
