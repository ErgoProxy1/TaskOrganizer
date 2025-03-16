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
    public class ProjectsController : ControllerBase
    {
        private readonly TaskOrganizerDbContext _dbContext;
        public ProjectsController(TaskOrganizerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var projects = await _dbContext.Projects.ToListAsync<ProjectModel>();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> Get(string projectId)
        {
            try
            {
                Guid parsedId;
                if (projectId != null && Guid.TryParse(projectId, out parsedId))
                {
                    var project = await _dbContext.Projects.FirstAsync(project => project.Id.Equals(parsedId));
                    if (project != null)
                    {
                        return Ok(project);
                    }
                    return NotFound(new { Error = "Project could not be found" }); 
                }
                return StatusCode(500, new { Error = "Project ID was null or invalid" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectDTO request)
        {
            try
            {
                var project = new ProjectModel
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    CreatedByUid = request.CreatedByUid,
                    Description = request.Description,
                };

                this._dbContext.Projects.Add(project);
                await _dbContext.SaveChangesAsync();
                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> Put(string projectId, [FromBody] ProjectDTO request)
        {
            try
            {
                Guid parsedId;
                if (Guid.TryParse(projectId, out parsedId))
                {
                    var project = new ProjectModel
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
                return StatusCode(500, new { Error = $"Project with Id {projectId} does not exist" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> Delete(string projectId)
        {
            try
            {
                Guid parsedId;
                if (projectId != null && Guid.TryParse(projectId, out parsedId))
                {
                    var project = await this._dbContext.Projects.FirstOrDefaultAsync(p => p.Id.Equals(parsedId));
                    if (project == null)
                    {
                        return NotFound(new { Error = $"Project with Id {projectId} does not exist" });
                    }
                    this._dbContext.Projects.Remove(project);
                    await _dbContext.SaveChangesAsync();
                    return Ok(); 
                }
                return StatusCode(500, new { Error = $"Project with Id {projectId} does not exist" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
