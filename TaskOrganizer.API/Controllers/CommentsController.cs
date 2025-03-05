using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskOrganizer.API.Controllers
{
  [Route("api/tasks/{taskId}/[controller]")]
  [ApiController]
  public class CommentsController : ControllerBase
  {
    // GET: api/tasks/5/comments
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] string taskId)
    {
      try
      {
        throw new NotImplementedException();
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // GET: api/tasks/5/comments/5
    [HttpGet("{commentId}")]
    public async Task<IActionResult> Get([FromRoute] string taskId, string commentId)
    {
      try
      {
        throw new NotImplementedException();
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // POST api/tasks/5/comments
    [HttpPost]
    public async Task<IActionResult> Post([FromRoute] string taskId, [FromBody] object? value)
    {
      try
      {
        throw new NotImplementedException();
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // PUT api/tasks/5/comments/5
    [HttpPut("{commentId}")]
    public async Task<IActionResult> Put([FromRoute] string taskId, string commentId, [FromBody] object? value)
    {
      try
      {
        throw new NotImplementedException();
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // DELETE api/tasks/5/comments/5
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> Delete([FromRoute] string taskId, string commentId)
    {
      try
      {
        throw new NotImplementedException();
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }
  }
}
