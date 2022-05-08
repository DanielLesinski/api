namespace Application.Dto.Announcements
{
    public class AddAnnouncementDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int AnnouncementTypeId { get; set; }
        //public int UserId { get; set; }
    }
}
