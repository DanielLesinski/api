using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ParkwayFunContext context;

        public AccountRepository(ParkwayFunContext context)
        {
            this.context = context;
        }

        public async Task RegisterUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public IQueryable<User> GetAllUsers()
        {
            return context.Users.Include(o => o.Role)
                .Include(o => o.Announcements).ThenInclude(o => o.Type)
                .Include(o => o.Announcements).ThenInclude(o => o.Detail);
        }

        public async Task<User?> GetById(int id)
        {
            return await GetAllUsers().SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateUser(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
