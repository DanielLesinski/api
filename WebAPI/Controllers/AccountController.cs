using Application.Dto;
using Application.Dto.Account;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [AllowAnonymous]
        [SwaggerOperation(Summary = "Register a new user")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            await accountService.RegisterUser(registerUserDto);
            return Ok();
        }

        [AllowAnonymous]
        [SwaggerOperation(Summary = "Login a user")]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDto loginUserDto)
        {
            string token = await accountService.GenerateJwt(loginUserDto);
            return Ok(token);
        }

        [SwaggerOperation(Summary = "Get all users")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await accountService.GetAllUser();
            if (users.Count == 0)
                return NotFound();
            return Ok(users);
        }

        [SwaggerOperation(Summary = "Search a user")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchByKeyword(string keyword)
        {
            var users = await accountService.SearchByKeyword(keyword);
            if (users.Count == 0)
                return NotFound();
            return Ok(users);
        }


        [SwaggerOperation(Summary = "Get info about user")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await accountService.GetUser(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [SwaggerOperation(Summary = "Get info about yourself")]
        [HttpGet("get")]
        public async Task<IActionResult> GetInfoAboutYourself()
        {
            var user = await accountService.GetInfoAboutYourself();
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [SwaggerOperation(Summary = "Update a user")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto update)
        {
            await accountService.UpdateUser(id, update);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete a user")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await accountService.DeleteUser(id);
            return NoContent();
        }
        
    }
}
