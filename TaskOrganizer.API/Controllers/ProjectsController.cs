﻿using FirebaseAdmin.Auth;
using Google.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskOrganizer.API.Data;
using TaskOrganizer.API.DTOs;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [AllowAnonymous]
  public class ProjectsController : ControllerBase
  {
    private readonly TaskOrganizerDbContext _dbContext;
    public ProjectsController(TaskOrganizerDbContext dbContext)
    {
      this._dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string userId)
    {
      try
      {
        var authorizationHeader = Request?.Headers?.Authorization.ToString();
        if (authorizationHeader != null && (await isAuthenticatedUser(authorizationHeader, userId)))
        {

          var query = _dbContext.Users.Where(u => u.Uid.Equals(userId)).SelectMany(u => u.UserProjects).Select(up => up.Project);
          var projects = await query.ToListAsync<Project>();
          return Ok(projects);
        }
        return Forbid("Bearer");

      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    //[HttpGet("{projectId}")]
    //TODO
    //public async Task<IActionResult> Get(string projectId, [FromQuery] string userId)
    //{
    //  try
    //  {
    //    Guid parsedId;
    //    if (projectId != null && Guid.TryParse(projectId, out parsedId))
    //    {
    //      var project = await _dbContext.Projects.FirstAsync(project => project.Id.Equals(parsedId));
    //      if (project != null)
    //      {
    //        return Ok(project);
    //      }
    //      return NotFound(new { Error = "Project could not be found" });
    //    }
    //    return StatusCode(500, new { Error = "Project ID was null or invalid" });
    //  }
    //  catch (Exception ex)
    //  {
    //    return BadRequest(new { Error = ex.Message });
    //  }
    //}

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProjectDTO request)
    {
      try
      {
        var user = await _dbContext.Users.Include(u => u.UserProjects).FirstOrDefaultAsync(u => u.Uid == request.CreatedByUid);
        if (user == null)
        {
          return NotFound(new { Error = "User not found" });
        }

        var authorizationHeader = Request?.Headers?.Authorization.ToString();
        if (authorizationHeader != null && (await isAuthenticatedUser(authorizationHeader, user.Uid)))
        {
            var project = new Project
          {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CreatedByUid = request.CreatedByUid,
            Description = request.Description,
          };
          project.UserProjects.Add(new UserProject { Project = project, User = user });

          _dbContext.Projects.Add(project);

          await _dbContext.SaveChangesAsync();

          return Ok(project);
        }
        return Forbid("Bearer");
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    [HttpPut("{projectId}")]
    public async Task<IActionResult> Put([FromQuery] string projectId, [FromQuery] string userId, [FromBody] ProjectDTO request)
    {
      try
      {
        var user = await _dbContext.Users.Include(u => u.UserProjects).FirstOrDefaultAsync(u => u.Uid == request.CreatedByUid);
        if (user == null)
        {
          return NotFound(new { Error = "User not found" });
        }
        var authorizationHeader = Request?.Headers?.Authorization.ToString();
        if (authorizationHeader != null && (await isAuthenticatedUser(authorizationHeader, userId)))
        {
          Guid parsedId;
          if (Guid.TryParse(projectId, out parsedId))
          {
            var project = new Project
            {
              Id = parsedId,
              Name = request.Name,
              CreatedByUid = request.CreatedByUid,
              Description = request.Description,
            };

            this._dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();
            return Ok(project);
          }
          return NotFound(new { Error = $"Project with Id {projectId} does not exist" });
        }
        return Forbid("Bearer");
      }
      catch (Exception ex)
      {
        return BadRequest(new { Error = ex.Message });
      }
    }

    //[HttpDelete("{projectId}")]
    //TODO
    //public async Task<IActionResult> Delete(string projectId)
    //{
    //  try
    //  {
    //    Guid parsedId;
    //    if (projectId != null && Guid.TryParse(projectId, out parsedId))
    //    {
    //      var project = await this._dbContext.Projects.FirstAsync(p => p.Id.Equals(parsedId));
    //      if (project == null)
    //      {
    //        return NotFound(new { Error = $"Project with Id {projectId} does not exist" });
    //      }
    //      this._dbContext.Projects.Remove(project);
    //      await _dbContext.SaveChangesAsync();
    //      return Ok();
    //    }
    //    return StatusCode(500, new { Error = $"Project with Id {projectId} does not exist" });
    //  }
    //  catch (Exception ex)
    //  {
    //    return BadRequest(new { Error = ex.Message });
    //  }
    //}

    private async Task<bool> isAuthenticatedUser(string jwt, string userId)
    {
      if (!string.IsNullOrWhiteSpace(jwt))
      {
        FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(jwt.Split(" ")[1]);
        string decodedUid = decodedToken.Uid;
        return decodedUid.Equals(userId); 
      }
      return false;
    }
  }
}
