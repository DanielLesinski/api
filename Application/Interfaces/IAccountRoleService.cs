using Application.Dto.AccountRole;

namespace Application.Interfaces
{
    public interface IAccountRoleService
    {
        Task<RoleOutDto> AddRole(RoleDto roleDto);
        Task<IList<RoleOutDto>> GetAllRoles();
        Task DeleteRole(int id);
    }
}
