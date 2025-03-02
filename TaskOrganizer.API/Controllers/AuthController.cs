using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.API.Contracts;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {
    private FirebaseAuth _fbauth;
    private FirestoreDb _firestoreDb;
    public AuthController(FirestoreDb firestoreDb)
    {
      _fbauth = FirebaseAuth.DefaultInstance;
      _firestoreDb = firestoreDb;
    }

    [HttpPost("verify-token")]
    public async Task<IActionResult> VerifyToken([FromBody] LoginContract request)
    {
      try
      {
        var decodedToken = await _fbauth.VerifyIdTokenAsync(request.IdToken);
        var snapshot = await this._firestoreDb.Collection("users").WhereEqualTo("uid", decodedToken.Uid).GetSnapshotAsync();
        string username = snapshot.Documents[0].GetValue<string>("username");
        decodedToken.Claims.TryGetValue("email", out object? emailClaim);
        return Ok(new User { Uid = decodedToken.Uid, Email = emailClaim?.ToString() ?? string.Empty, Username = username});
      }
      catch (Exception ex)
      {
        return BadRequest(new {Error = ex.Message});
      }
    }
  }
}
