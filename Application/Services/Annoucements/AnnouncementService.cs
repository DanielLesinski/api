using Application.Authorization;
using Application.Dto.Announcements;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Announcements;
using AutoMapper;
using Domain.Entities.Announcements;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Application.Services.Annoucements
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository announcementRepository;
        private readonly IMapper mapper;
        private readonly IAccountRepository accountRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly IUserContextService userContextService;

        public AnnouncementService(IAnnouncementRepository announcementRepository,IMapper mapper
            ,IAccountRepository accountRepository, IAuthorizationService authorizationService
            ,IUserContextService userContextService)
        {
            this.mapper = mapper;
            this.announcementRepository = announcementRepository;
            this.accountRepository = accountRepository;
            this.authorizationService = authorizationService;
            this.userContextService = userContextService;
        }

        public async Task<AnnouncementListDto> GetAllAnnouncement()
        {
            var announcement = announcementRepository.GetAll();
            return mapper.Map<AnnouncementListDto>(announcement);
        }

        public async Task<AnnouncementDto> GetAnnouncementById(int id)
        {
            var announcement = await announcementRepository.GetById(id);
            return mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task<AnnouncementListDto> SearchbyKeyword(string keyword)
        {
            keyword = keyword.ToLowerInvariant();
            var announcement = announcementRepository.GetAll()
                .Where(o => o.Title.ToLower().Contains(keyword) || o.Content.ToLower().Contains(keyword)).ToList();
            return mapper.Map<AnnouncementListDto>(announcement);
        }

        public async Task<AnnouncementDto> AddNewAnnouncement(AddAnnouncementDto newAnnouncement)
        {
            var announcement = mapper.Map<Announcement>(newAnnouncement);

            announcement.Detail = new AnnouncementDetail
            {
                Created = DateTime.Now,
                LastModified = DateTime.Now,
            };

            announcement.UserId = (int)userContextService.GetUserId;

            announcement = await announcementRepository.Add(announcement);
            return mapper.Map<AnnouncementDto>(announcement);
        }

        public async Task UpdateAnnouncement(int id, UpdateAnnouncementDto announcement)
        {
            var existingAnnouncement = await announcementRepository.GetById(id);

            var authorizationResult = await authorizationService.AuthorizeAsync(
                userContextService.User, existingAnnouncement,
                new ResourceOperationRequirement(ResorceOperation.Update));

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Frobidden");

            var updatedAnnouncement = mapper.Map(announcement, existingAnnouncement);

            updatedAnnouncement.Detail.LastModified = DateTime.Now;

            await announcementRepository.Update(updatedAnnouncement);
        }

        public async Task<AnnouncementListDtoWithoutUser> GetAllUserAnnouncement(int id)
        {
            var user = await accountRepository.GetById(id);
            if (user == null)
                throw new NotFoundException("Not found a resources");
            var announcements = user.Announcements;
            return mapper.Map<AnnouncementListDtoWithoutUser>(announcements);
        }

        public async Task DeleteAnnouncement(int id)
        {
            var announcement = await announcementRepository.GetById(id);

            var authorizationResult = await authorizationService.AuthorizeAsync(
                userContextService.User, announcement,
                new ResourceOperationRequirement(ResorceOperation.Delete));

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Frobidden");

            await announcementRepository.Delete(announcement);
        }
    }
}
