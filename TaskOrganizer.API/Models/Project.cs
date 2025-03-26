using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskOrganizer.API.Models
{
    public class Project
    {
        [Key]
        public required Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [MaxLength(400)]
        public string Description { get; set; } = "";
        [Required]
        public required string CreatedByUid { get; init; }

        // Navigation
        [JsonIgnore]
        public List<UserProject> UserProjects { get; } = [];
    }
}
