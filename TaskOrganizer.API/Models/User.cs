using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrganizer.API.Models
{
  [FirestoreData]
  public class User
  {
    [FirestoreProperty(Name = "email")]
    public string Email { get; set; } = "";
    [FirestoreProperty(Name = "username")]
    public string Username { get; set; } = "";
    [FirestoreProperty(Name = "uid")]
    public string Uid { get; set; } = "";
  }

}
