using System;
using System.Collections.Generic;
using System.Linq;
namespace Domain.Entities.Announcements
{
    public class AnnouncementDetail
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

        public int AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }
    }
}
