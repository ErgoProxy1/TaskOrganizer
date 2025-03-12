using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using TaskOrganizer.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskOrganizer.API.Controllers
{
  [Route("api/tasks/{taskId}/[controller]")]
  [ApiController]
  public class CommentsController : ControllerBase
  {
    private FirestoreDb _firestoreDb;
    public CommentsController(FirestoreDb firestoreDb)
    {
      _firestoreDb = firestoreDb;
    }


    // GET: api/tasks/5/comments
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] string taskId)
    {
      try
      {
        var snapshot = await _firestoreDb.Collection("tasks").Document(taskId).Collection("comments").GetSnapshotAsync();
        List<CommentModel> comments = new List<CommentModel>();
        foreach (var commentDocument in snapshot.Documents)
        {
          if (commentDocument.Exists)
          {
            CommentModel comment = commentDocument.ConvertTo<CommentModel>();
            comment.Id = commentDocument.Id;
            comments.Add(comment);
          }
        }
        return Ok(comments);
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
        if (taskId != null && commentId != null)
        {
          var commentDocument = await _firestoreDb.Collection("tasks").Document(taskId).Collection("comments").Document(commentId).GetSnapshotAsync();
          if (commentDocument.Exists)
          {
            CommentModel comment = commentDocument.ConvertTo<CommentModel>();
            comment.Id = commentDocument.Id;
            comment.TaskId = taskId;
            return Ok(comment);
          }
          return NotFound();
        }
        return StatusCode(500, "Task or Comment Id was null or invalid");
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // POST api/tasks/5/comments
    [HttpPost]
    public async Task<IActionResult> Post([FromRoute] string taskId, [FromBody] CommentModel comment)
    {
      try
      {
        var docRef = await _firestoreDb.Collection("tasks").Document(taskId).Collection("comments").AddAsync(comment);
        var commentDocument = await docRef.GetSnapshotAsync();
        if (!commentDocument.Exists)
        {
          return StatusCode(500, new { Error = $"Comment Creation under Task {taskId} Failed" });
        }
        CommentModel createdComment = commentDocument.ConvertTo<CommentModel>();
        createdComment.Id = commentDocument.Id;
        createdComment.TaskId = taskId;
        return Ok(createdComment);
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // PUT api/tasks/5/comments/5
    [HttpPut("{commentId}")]
    public async Task<IActionResult> Put([FromRoute] string taskId, string commentId, [FromBody] CommentModel comment)
    {
      try
      {
        var docRef = _firestoreDb.Collection("tasks").Document(taskId).Collection("comments").Document(commentId);
        var commentDocument = await docRef.GetSnapshotAsync();
        if (!commentDocument.Exists)
        {
          return StatusCode(500, new { Error = $"Comment update Failed as a Comment with ID {commentId} does not exist under Task {taskId}" });
        }
        await docRef.SetAsync(comment);
        comment.Id = commentId;
        comment.TaskId = taskId;
        return Ok(comment);
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
        var docRef = _firestoreDb.Collection("tasks").Document(taskId).Collection("comments").Document(commentId);
        if (!(await docRef.GetSnapshotAsync()).Exists)
        {
          return StatusCode(500, new { Error = $"Comment Delete Failed as a Comment with ID {commentId} does not exist under Task {taskId}" });
        }
        await docRef.DeleteAsync();
        return Ok(taskId);
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }
  }
}
