using Application.Dto;
using Application.Dto.Account;
using Application.Dto.AccountRole;
using Application.Dto.Announcements;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Announcements;

namespace Application.Mappings
{
    public static class AutoMappingConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                #region Account

                cfg.CreateMap<RegisterUserDto, User>();
                cfg.CreateMap<User, GetUserDto>();
                cfg.CreateMap<UpdateUserDto, User>();
                cfg.CreateMap<IEnumerable<User>, ListUserDto>()
                .ForMember(dest => dest.Users, act => act.MapFrom(src => src))
                .ForMember(dest => dest.Count, act => act.MapFrom(src => src.Count()));

                #endregion

                #region AccountRole

                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<Role, RoleOutDto>();

                #endregion


                #region Announcement

                cfg.CreateMap<Announcement, AnnouncementDto>()
                    .ForMember(dest => dest.LastModified, act => act.MapFrom(src => src.Detail.LastModified))
                    .ForMember(dest => dest.AnnouncementType, act => act.MapFrom(src => src.Type.Name));
                cfg.CreateMap<Announcement, AnnouncementDtoWithoutUser>()
                    .ForMember(dest => dest.LastModified, act => act.MapFrom(src => src.Detail.LastModified))
                    .ForMember(dest => dest.AnnouncementType, act => act.MapFrom(src => src.Type.Name));
                cfg.CreateMap<AddAnnouncementDto, Announcement>();
                cfg.CreateMap<UpdateAnnouncementDto, Announcement>();
                cfg.CreateMap<IEnumerable<Announcement>, AnnouncementListDto>()
                .ForMember(dest => dest.AnnouncementsDto, act => act.MapFrom(src => src))
                .ForMember(dest => dest.Count, act => act.MapFrom(src => src.Count()));
                cfg.CreateMap<IEnumerable<Announcement>, AnnouncementListDtoWithoutUser>()
                .ForMember(dest => dest.AnnouncementsDto, act => act.MapFrom(src => src))
                .ForMember(dest => dest.Count, act => act.MapFrom(src => src.Count()));

                #endregion

                #region AnnouncementType

                cfg.CreateMap<AnnouncementType, AnnouncementTypeDto>();
                cfg.CreateMap<AddAnnouncementTypeDto, AnnouncementType>();
                cfg.CreateMap<UpdateAnnouncementTypeDto, AnnouncementType>();

                #endregion

            }).CreateMapper();
    }
}
