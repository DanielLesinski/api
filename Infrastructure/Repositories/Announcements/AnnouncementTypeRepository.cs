using Domain.Entities.Announcements;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Announcements
{
    public class AnnouncementTypeRepository : IAnnouncementTypeRepository
    {
        private readonly ParkwayFunContext context;

        public AnnouncementTypeRepository(ParkwayFunContext context)
        {
            this.context = context;
        }

        public IQueryable<AnnouncementType> GetAll()
        {
            return context.AnnouncementTypes.Include(o => o.Announcements);
        }

        public async Task<AnnouncementType> GetById(int id)
        {
            return await GetAll().SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<AnnouncementType> Add(AnnouncementType announcementType)
        {
            context.AnnouncementTypes.Add(announcementType);
            await context.SaveChangesAsync();
            return announcementType;
        }

        public async Task Update(AnnouncementType announcementType)
        {
            context.AnnouncementTypes.Update(announcementType);
            await context.SaveChangesAsync();
        }

        public async Task Delete(AnnouncementType announcementType)
        {
            context.AnnouncementTypes.Remove(announcementType);
            await context.SaveChangesAsync();
        }
    }
}
