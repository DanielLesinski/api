using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAccountRoleRepository
    {
        IQueryable<Role> GetAllRoles();
        Task<Role> AddRole(Role role);
        Task DeleteRole(Role role);
        Task<Role> GetRoleById(int id);
    }
}
