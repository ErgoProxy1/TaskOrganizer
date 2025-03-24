using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskOrganizer.API.Models
{
    public class User
    {
        [Key]
        [Required]
        public required string Uid { get; set; }
        public string ProfilePictureUrl { get; set; } = "";
        [Required]
        public string Role { get; set; } = "user";

        // Navigation
        [JsonIgnore]
        public List<Project> Projects { get; } = [];
        [JsonIgnore]
        public List<UserProject> UserProjects { get; } = [];

    }

}
