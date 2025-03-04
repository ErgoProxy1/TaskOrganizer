using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.API.Models
{
  public class UserModel
  {
    public string Email { get; set; } = "";
    public string Username { get; set; } = "";
    public string Uid { get; set; } = "";
  }

}
