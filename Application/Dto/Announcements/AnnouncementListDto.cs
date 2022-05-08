namespace Application.Dto.Announcements
{
    public class AnnouncementListDto
    {
        public int Count { get; set; }
        public IList<AnnouncementDto> AnnouncementsDto { get; set; }
    }
}
