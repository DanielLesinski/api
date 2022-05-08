using Application.Dto.Announcements;

namespace Application.Interfaces.Announcements
{
    public interface IAnnouncementTypeService
    {
        Task<IList<AnnouncementTypeDto>> GetAllAnnouncementTypes();
        Task<AnnouncementTypeDto> GetAnnouncementTypeById(int id);
        Task<AnnouncementTypeDto> AddNewAnnouncementType(AddAnnouncementTypeDto newType);
        Task UpdateAnnouncementType(int id, UpdateAnnouncementTypeDto updateAnnouncementType);
        Task DeleteAnnouncementType(int id);
    }
}
