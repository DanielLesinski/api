namespace Application.Dto.Announcements
{
    public class AnnouncementListDtoWithoutUser
    {
        public int Count { get; set; }
        public IList<AnnouncementDtoWithoutUser> AnnouncementsDto { get; set; }
    }
}
