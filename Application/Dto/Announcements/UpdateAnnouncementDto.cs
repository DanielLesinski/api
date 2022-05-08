namespace Application.Dto.Announcements
{
    public class UpdateAnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AnnouncementTypeId { get; set; }
    }
}
