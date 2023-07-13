using ZigitApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ZigitApi.Utils;


namespace ZigitApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly DBContext DBContext;

        public ProjectController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }

        //GET progect controller that return the project that belong to the authnticted user id
        [HttpGet("GetProjects")]
        public async Task<ActionResult<Project>> Login(int Id)
        {

            //get the token sent by the client reqeset
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader?.Replace("Bearer ", "");

            //check if the record of the user id who sent the reqest has the same sent token value
            //if no - return error message
            var user = await DBContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if(user.Token!=token)
            {
                return BadRequest((new { error = "There is no projects for the user id" }));
            }

            //run of all the proejcts that belongs for the user and return them
            List<Project> projects = await DBContext.Projects
        .Where(p => p.userId == Id)
        .ToListAsync();
            if (projects == null || projects.Count==0 )
            {
                return BadRequest((new { error = "There is no projects for the user id" }));
            }
            else
            {
                return Ok(projects);
            }
        }


        //Insert Users to data base as a Seed option
        [HttpPost("InsertProjects")]
        public async Task<HttpStatusCode> InsertProjects()
        {
            var projectsArray = new Project[]
 {
               new Project {Id = 1,Name = "Backend Project",Score = 92, DurationInDays = 25,BugsCount = 72 ,MadeDeadline = true, userId=1 },
               new Project {Id = 2,Name = "Fronted Project" ,Score = 100, DurationInDays = 35,BugsCount = 60 ,MadeDeadline = true, userId=2 },
               new Project {Id = 3,Name = "Fronted Projec",Score = 60, DurationInDays = 30,BugsCount = 20 ,MadeDeadline = false, userId=3 },
               new Project {Id = 4,Name = "Fullstack Project",Score = 75, DurationInDays = 10,BugsCount = 40 ,MadeDeadline = true, userId=1 },
               new Project {Id = 5,Name = "Backend Project",Score = 92, DurationInDays = 8, BugsCount = 23 ,MadeDeadline = false, userId=2 },
               new Project {Id = 6,Name = "Backend Project",Score = 65, DurationInDays = 5, BugsCount = 10 ,MadeDeadline = true, userId=3 },
               new Project {Id = 7,Name = "Fullstack Project",Score = 78, DurationInDays = 56,BugsCount = 90 ,MadeDeadline = true, userId=1 },
               new Project {Id = 8,Name = "Fullstack Project",Score = 95, DurationInDays = 15,BugsCount = 15 ,MadeDeadline = true, userId=1 },
               new Project {Id = 9,Name = "Backend Project",Score = 85, DurationInDays = 12,BugsCount = 10 ,MadeDeadline = false, userId=2 },
               new Project {Id = 10,Name = "Fronted Projecm",Score = 73, DurationInDays = 2, BugsCount = 18 ,MadeDeadline = true, userId=3 },
           };


            foreach (var project_to_insert in projectsArray) {
                DBContext.Projects.Add(project_to_insert);
                await DBContext.SaveChangesAsync();
            }
            return HttpStatusCode.Created;
        }

 
    }
}
