using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskOrganizer.API.Data;
using TaskOrganizer.API.DTOs;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors("MyPolicy")]
  [AllowAnonymous]
  public class AuthController : ControllerBase
  {
    private FirebaseAuth _fbauth;
    private TaskOrganizerDbContext _dbContext;
    public AuthController(TaskOrganizerDbContext dbContext)
    {
      _fbauth = FirebaseAuth.DefaultInstance;
      _dbContext = dbContext;
    }

    [EnableCors("MyAllowSpecificOrigins")]
    [HttpPost("verify-token")]
    public async Task<IActionResult> VerifyToken([FromBody] VerifyTokenRequest request)
    {
      try
      {
        var decodedToken = await _fbauth.VerifyIdTokenAsync(request.IdToken);
        decodedToken.Claims.TryGetValue("email", out object? emailClaim);
        decodedToken.Claims.TryGetValue("name", out object? nameClaim);
        return base.Ok(new DTOs.UserResponseDTO { Uid = decodedToken?.Uid ?? string.Empty, Email = emailClaim?.ToString() ?? string.Empty, DisplayName = nameClaim?.ToString() ?? string.Empty });
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] SignupRequest request)
    {
      UserRecord? user = null;
      try
      {
        
        user = await _fbauth.CreateUserAsync(new UserRecordArgs
        {
          Email = request.Email,
          Password = request.Password,
          DisplayName = request.Username,
          PhotoUrl = "",
        });
        
        _dbContext.Users.Add(new User
        {
          Uid = user.Uid,
          DisplayName = request.Username,
          Email = request.Email,
          PhotoUrl = ""
        });

        await _dbContext.SaveChangesAsync();
        return Ok();
      }
      catch (Exception ex)
      {
        if (user != null)
        {
          await _fbauth.DeleteUserAsync(user.Uid);
        }
        return BadRequest(new { Error = ex.Message });
      }
    }
  }
}
