using Domain.Entities.Announcements;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }


        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Announcement> Announcements { get; set; }
    }
}
