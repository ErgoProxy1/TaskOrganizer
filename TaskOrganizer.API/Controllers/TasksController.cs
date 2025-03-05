using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskOrganizer.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskOrganizer.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TasksController : ControllerBase
  {
    private FirebaseAuth _fbauth;
    private FirestoreDb _firestoreDb;
    public TasksController(FirestoreDb firestoreDb)
    {
      _fbauth = FirebaseAuth.DefaultInstance;
      _firestoreDb = firestoreDb;
    }

    // GET: api/tasks
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      try
      {
        var snapshot = await _firestoreDb.Collection("tasks").GetSnapshotAsync();
        List<TaskModel> tasks = new List<TaskModel>();
        foreach (DocumentSnapshot document in snapshot.Documents)
        {
          if(document.Exists)
          {
            TaskModel currentTask = document.ConvertTo<TaskModel>();
            currentTask.TaskId = document.Id;
            tasks.Add(currentTask);
          }
        }
        return Ok(tasks);
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // GET api/tasks/5
    [HttpGet("{taskId}")]
    public async Task<IActionResult> Get(string taskId)
    {
      try
      {
        if (taskId != null)
        {
          var document = await _firestoreDb.Collection("tasks").Document(taskId).GetSnapshotAsync();
          if (document.Exists)
          {
            TaskModel task = document.ConvertTo<TaskModel>();
            task.TaskId = document.Id;
            return Ok(task);
          }
          return NotFound(new { Error = "Resource could not be found" });
        }
        return BadRequest(new { Error = "Task ID was null or invalid." });
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // POST api/tasks
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/tasks/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/tasks/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
