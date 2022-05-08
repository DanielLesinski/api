using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly ParkwayFunContext context;

        public AccountRoleRepository(ParkwayFunContext context)
        {
            this.context = context;
        }

        public IQueryable<Role> GetAllRoles()
        {
            return context.Roles.Include(o => o.Users);
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await GetAllRoles().SingleOrDefaultAsync(o => o.Id == id);
            return role;
        }

        public async Task<Role> AddRole(Role role)
        {
            context.Roles.Add(role);
            await context.SaveChangesAsync();
            return role;
        }

        public async Task DeleteRole(Role role)
        {
            context.Roles.Remove(role);
            await context.SaveChangesAsync();
        }
       
    }
}
