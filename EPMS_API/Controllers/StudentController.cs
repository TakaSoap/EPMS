using EPMS_API.Models.StudentManagement;
using EPMS_API.Services;
using EPMS_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Authing.ApiClient.Domain.Model.Management.Users;
using Authing.ApiClient.Domain.Model;
using User = EPMS_API.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EPMS_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly UserService _userService;

        public StudentController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public async Task<List<StudentProcessed>> Get()
        {
            var studentsProcessed = new List<StudentProcessed>();

            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            string baseId = await GetBaseId(token);

            var students = await _userService.GetStudentsAsync(baseId);

            foreach(var student in students)
            {
                var studentProcessed = new StudentProcessed(student.Id, student.Name, student.PhoneNumber, student.Email);
                studentsProcessed.Add(studentProcessed);
            }

            return studentsProcessed;
        }

        [HttpGet("base")]
        public async Task<string> GetBase()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            string baseId = await GetBaseId(token);

            var internshipBase = await _userService.GetBaseAsync(baseId);

            if (internshipBase == null)
            {
                return "null";
            }

            return internshipBase.BaseName;
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post(StudentSubmit studentSubmit)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString()[7..];

            string baseId = await GetBaseId(token);

            var user = new User();
            user.Name = studentSubmit.name;
            user.PhoneNumber = studentSubmit.phoneNumber;
            user.Email = studentSubmit.email;
            user.UserType = "student";
            user.baseId = baseId;

            var authingUser = await AuthingClient.Client.Users.Create(new CreateUserInput
            {
                Username = studentSubmit.username,
                Name = studentSubmit.name,
                Password = studentSubmit.password,
                Email = studentSubmit.email,
                Phone = studentSubmit.phoneNumber,
                Profile = "student"
            });

            user.UserIdAuthing = authingUser.Id;

            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(Get), new {id=user.Id}, user);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, StudentProcessed student)
        {
            var updatedStudent = new User();

            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedStudent = user;
            updatedStudent.Name = student.name;
            updatedStudent.PhoneNumber = student.phoneNumber;
            updatedStudent.Email = student.email;

            Console.WriteLine(user.UserIdAuthing);

            var updates = new UpdateUserInput()
            {
                Email = student.email,
                Phone = student.phoneNumber,
                Name = student.name,
            };

            await AuthingClient.Client.Users.Update(user.UserIdAuthing, updates);

            await _userService.UpdateAsync(id, updatedStudent);

            return Ok();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete(StudentDeleteId ids)
        {
            foreach(var id in ids.ids)
            {
                var user = await _userService.GetAsync(id);

                if(user == null)
                {
                    return NotFound();
                }

                await AuthingClient.Client.Users.Delete(user.UserIdAuthing);

                await _userService.RemoveAsync(id);
            }

            return Ok();
        }


        private async Task<string> GetBaseId(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            var claimValue = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            Console.WriteLine(claimValue);

            if (claimValue != null)
            {
                var user = await _userService.GetUserByAuthingId(claimValue);

                if (user != null)
                {
                    return user.baseId;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}
