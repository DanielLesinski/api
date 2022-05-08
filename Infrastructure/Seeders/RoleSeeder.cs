using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Seeders
{
    public class RoleSeeder
    {
        private readonly ParkwayFunContext context;

        public RoleSeeder(ParkwayFunContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            if(context.Database.CanConnect())
            {
                if(!context.Roles.Any())
                {
                    var roles = GetRoles();
                    context.Roles.AddRange(roles);
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role(){Name = "User"},
                new Role(){Name = "Moderator"},
                new Role(){Name = "Admin"}
            };
            return roles;
        }
    }
}
