using ZigitApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using ZigitApi.Utils;

namespace ZigitApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBContext DBContext;

        public UserController(DBContext DBContext)
        {
            this.DBContext = DBContext;
        }


        //login controller as endpoint of the login form in the client side
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(Login login_req )
        {
            //check if there is email and password match and if no - erturn error
            var user = await DBContext.Users.
                FirstOrDefaultAsync(s => s.Email == login_req.Email && s.Password==login_req.Password);

            if (user == null)
            {
                return BadRequest((new { error = "Incorrect email or password" }));
            }
            else
            {
                //generating token through JWT
                string token = Authentication.GenerateJwtToken(user.Id.ToString(), user.Name, "xxte324324xbndmn1502opdfkds106lmpodxc546");

                // ADD the token string and its creation time to the User table in the database 
                user.Token = token; user.Token_created_at = DateTime.Now;
                await DBContext.SaveChangesAsync();

                //retung the user details in json format and the token string
                return Ok(LoginApiResponse.GenerateApiResponse(token,user));
            }
        }


        //Insert Users to data base as a Seed option
            [HttpPost("InsertUsers")]
        public async Task<HttpStatusCode> InsertUsers()
        {
            var usersArray = new User[]
 {
               new User {Id = 1,Email = "a@a.com",Password = "Aa123456", Name = "Omer",Team = "Developers",joined = "2023-04-10", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
               new User {Id = 2,Email = "b@b.com",Password = "Aa123456", Name = "Moshe",Team = "Developers",joined = "2023-05-01", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
               new User {Id = 3,Email = "c@c.com",Password = "Aa123456", Name = "Yossi",Team = "Developers",joined = "2023-05-01", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
               new User {Id = 4,Email = "d@d.com",Password = "Aa123456", Name = "Omer",Team = "Developers",joined = "2023-04-01", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
               new User {Id = 5,Email = "e@e.com",Password = "Aa123456", Name = "Omer",Team = "Developers",joined = "2023-04-10", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
               new User {Id = 6,Email = "f@f.com",Password = "Aa123456", Name = "Omer",Team = "Developers",joined = "2023-05-01", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
               new User {Id = 7,Email = "g@g.com",Password = "Aa123456", Name = "Omer",Team = "Developers",joined = "2023-05-01", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
               new User {Id = 8,Email = "h@h.com",Password = "Aa123456", Name = "Omer",Team = "Developers",joined = "2023-04-10", Avatar = "https://private-052d6-testapi4528.apiary-mock.com/authenticate" },
 };


            foreach (var user_to_insert in usersArray) {
                DBContext.Users.Add(user_to_insert);
                await DBContext.SaveChangesAsync();
            }
            return HttpStatusCode.Created;
        }

 
    }
}
