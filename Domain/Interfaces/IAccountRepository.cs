using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task RegisterUser(User user);
        IQueryable<User> GetAllUsers();
        Task<User?> GetById(int id);
        Task DeleteUser(User user);
        Task UpdateUser(User user);
    }
}
