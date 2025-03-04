using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.API.Contracts;

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
    public async Task<IActionResult> VerifyToken([FromBody] VerifyTokenContract request)
    {
      try
      {
        var decodedToken = await _fbauth.VerifyIdTokenAsync(request.IdToken);
        decodedToken.Claims.TryGetValue("email", out object? emailClaim);
        decodedToken.Claims.TryGetValue("name", out object? nameClaim);
        return Ok(new { Uid = decodedToken?.Uid ?? string.Empty, Email = emailClaim?.ToString() ?? string.Empty, Username = nameClaim?.ToString() ?? string.Empty});
      }
      catch (Exception ex)
      {
        return BadRequest(new {Error = ex.Message});
      }
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] SignupContract request) 
    {
      try
      {
        var user = await _fbauth.CreateUserAsync(new UserRecordArgs { Email = request.Email, Password = request.Password, DisplayName = request.Username });
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }
  }
}
