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
    [Required]
    public required string PhotoUrl { get; set; }
    [Required]
    public string Role { get; set; } = "user";
    [Required]
    public required string DisplayName { get; set; }
    [Required]
    public required string Email { get; set; }

    // Navigation
    [JsonIgnore]
    public List<UserProject> UserProjects { get; } = [];

  }

}
