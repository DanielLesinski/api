using Domain.Entities.Announcements;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Announcements
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ParkwayFunContext context;

        public AnnouncementRepository(ParkwayFunContext context)
        {
            this.context = context;
        }

        public IQueryable<Announcement> GetAll()
        {
            return context.Announcements.Include(x => x.User).Include(x => x.Detail)
                .Include(x => x.Type);
        }

        public async Task<Announcement> GetById(int id)
        {
            return await GetAll().SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Announcement> Add(Announcement announcement)
        {
            context.Announcements.Add(announcement);
            await context.SaveChangesAsync();
            var ann = await GetById(announcement.Id);
            return ann;
        }

        public async Task Update(Announcement announcement)
        {
            context.Announcements.Update(announcement);
            await context.SaveChangesAsync();

        }

        public async Task Delete(Announcement announcement)
        {
            context.Announcements.Remove(announcement);
            await context.SaveChangesAsync();
        }
    }
}
