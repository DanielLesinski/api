using Application.Dto.AccountRole;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleService accountRoleService;

        public AccountRoleController(IAccountRoleService accountRoleService)
        {
            this.accountRoleService = accountRoleService;
        }

        [SwaggerOperation(Summary = "Add a new role (only admin)")]
        [HttpPost("add")]
        public async Task<IActionResult> AddRole(RoleDto roleDto)
        {
            var role = await accountRoleService.AddRole(roleDto);
            return Ok(role);
        }


        [SwaggerOperation(Summary = "Delete a role (only admin)")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await accountRoleService.DeleteRole(id);
            return NoContent();
        }


        [SwaggerOperation(Summary = "Get all roles (only admin)")]
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await accountRoleService.GetAllRoles();
            if (roles == null)
                return NotFound();
            return Ok(roles);
        }
    }
}
