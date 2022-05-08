using Domain.Entities.Announcements;

namespace Domain.Interfaces
{
    public interface IAnnouncementTypeRepository
    {
        IQueryable<AnnouncementType> GetAll();
        Task<AnnouncementType> GetById(int id);
        Task<AnnouncementType> Add(AnnouncementType announcementType);
        Task Update(AnnouncementType announcementType);
        Task Delete(AnnouncementType announcementType);
    }
}
