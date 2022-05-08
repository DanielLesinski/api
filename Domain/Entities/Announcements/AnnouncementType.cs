namespace Domain.Entities.Announcements
{
    public class AnnouncementType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Announcement> Announcements{ get; set; }
    }
}
