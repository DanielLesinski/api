using Application.Dto.Announcements;
using Application.Interfaces.Announcements;
using AutoMapper;
using Domain.Entities.Announcements;
using Domain.Interfaces;

namespace Application.Services.Annoucements
{
    public class AnnouncemetTypeService : IAnnouncementTypeService
    {
        private readonly IAnnouncementTypeRepository announcementTypeRepository;
        private readonly IMapper mapper;

        public AnnouncemetTypeService(IAnnouncementTypeRepository announcementTypeRepository, IMapper mapper)
        {
            this.announcementTypeRepository = announcementTypeRepository;
            this.mapper = mapper;
        }

        public async Task<IList<AnnouncementTypeDto>> GetAllAnnouncementTypes()
        {
            var types = announcementTypeRepository.GetAll();
            return mapper.Map<IList<AnnouncementTypeDto>>(types);
        }

        public async Task<AnnouncementTypeDto> GetAnnouncementTypeById(int id)
        {
            var tape = await announcementTypeRepository.GetById(id);
            return mapper.Map<AnnouncementTypeDto>(tape);
        }

        public async Task<AnnouncementTypeDto> AddNewAnnouncementType(AddAnnouncementTypeDto newType)
        {
            var type = mapper.Map<AnnouncementType>(newType);
            type = await announcementTypeRepository.Add(type);
            return mapper.Map<AnnouncementTypeDto>(type);
        }


        public async Task UpdateAnnouncementType(int id, UpdateAnnouncementTypeDto updateAnnouncementType)
        {
            var existingType = await announcementTypeRepository.GetById(id);
            var updatedType = mapper.Map(updateAnnouncementType, existingType);

            await announcementTypeRepository.Update(updatedType);
        }
        
        public async Task DeleteAnnouncementType(int id)
        {
            var type = await announcementTypeRepository.GetById(id);
            await announcementTypeRepository.Delete(type);
        }

        
    }
}
