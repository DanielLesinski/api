using Application.Dto.Account;

namespace Application.Dto.Announcements
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
        public string AnnouncementType { get; set; }

        public GetUserDto User { get; set; }
    }
}
