using Application.Dto.Announcements;

namespace Application.Interfaces.Announcements
{
    public interface IAnnouncementService
    {
        Task<AnnouncementListDto> GetAllAnnouncement();
        Task<AnnouncementListDto> SearchbyKeyword(string keyword);
        Task<AnnouncementDto> GetAnnouncementById(int id);
        Task<AnnouncementDto> AddNewAnnouncement(AddAnnouncementDto newAnnouncement);
        Task UpdateAnnouncement(int id, UpdateAnnouncementDto announcement);
        Task DeleteAnnouncement(int id);
        Task<AnnouncementListDtoWithoutUser> GetAllUserAnnouncement(int id);
    }
}
