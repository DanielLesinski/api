namespace Domain.Entities.Announcements
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public virtual AnnouncementDetail Detail { get; set; }

        public int AnnouncementTypeId { get; set; }
        public virtual AnnouncementType Type { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
