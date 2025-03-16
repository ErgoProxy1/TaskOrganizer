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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProjectDTO request)
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

    }
}
