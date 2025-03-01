using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2.Requests;
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
    AuthController()
    {
      _fbauth = FirebaseAuth.DefaultInstance;
    }

    [HttpPost("verify-token")]
    public async Task<IActionResult> VerifyToken([FromBody] LoginContract request)
    {
      try
      {
        var decodedToken = await _fbauth.VerifyIdTokenAsync(request.IdToken);
        return Ok(new LoginResponseContract { Uid = decodedToken.Uid });
      }
      catch (Exception ex)
      {
        return BadRequest(new {Error = ex.Message});
      }
    }
  }
}
