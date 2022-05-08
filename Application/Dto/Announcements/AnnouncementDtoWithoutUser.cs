namespace Application.Dto.Announcements
{
    public class AnnouncementDtoWithoutUser
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
        public string AnnouncementType { get; set; }
    }
}
