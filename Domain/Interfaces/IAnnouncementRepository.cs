using Domain.Entities.Announcements;

namespace Domain.Interfaces
{
    public interface IAnnouncementRepository
    {
        IQueryable<Announcement> GetAll();
        Task<Announcement> GetById(int id);
        Task<Announcement> Add(Announcement announcement);
        Task Update(Announcement announcement);
        Task Delete(Announcement announcement);
    }
}
