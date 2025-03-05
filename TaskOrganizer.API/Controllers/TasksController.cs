using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http.HttpResults;
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
        return StatusCode(500, new { Error = "Task ID was null or invalid" });
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // POST api/tasks
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TaskModel task)
    {
      try
      {
        var documentRef = await _firestoreDb.Collection("tasks").AddAsync(task);
        var documentSnapshot = await documentRef.GetSnapshotAsync();
        if (!documentSnapshot.Exists)
        {
          return StatusCode(500, new { Error = "Task Creation Failed" });
        }
        TaskModel createdTask = documentSnapshot.ConvertTo<TaskModel>();
        createdTask.TaskId = documentSnapshot.Id;
        // TODO sub-collections
        return Ok(createdTask);
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    // PUT api/tasks/5
    [HttpPut("{taskId}")]
    public async Task<IActionResult> Put(string taskId, [FromBody] TaskModel task)
    {
      try
      {
        var docRef = _firestoreDb.Collection("tasks").Document(taskId);
        var documentSnapshot = await docRef.GetSnapshotAsync();
        if (!documentSnapshot.Exists)
        {
          return StatusCode(500, new { Error = "Task Update Failed as a Task with this ID does not exist" });
        }
        await docRef.SetAsync(task);
        task.TaskId = taskId;
        return Ok(task);
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }

    }

    // DELETE api/tasks/5
    [HttpDelete("{taskId}")]
    public async Task<IActionResult> Delete(string taskId)
    {
      try
      {
        var docRef = _firestoreDb.Collection("tasks").Document(taskId);
        if(!(await docRef.GetSnapshotAsync()).Exists)
        {
          return StatusCode(500, new { Error = "Task Delete Failed as a Task with this ID does not exist" });
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
