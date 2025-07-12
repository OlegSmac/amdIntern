using AutoMapper;
using Vehicles.API.Contracts.DTOs.Notifications;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.API.Models.Profiles.Notifications;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<UserNotification, UserNotificationDTO>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body))
            .ForMember(dest => dest.CreatedAt, src => src.MapFrom(x => x.CreatedAt))
            .ForMember(dest => dest.IsRead, src => src.MapFrom(x => x.IsRead))
            .ForMember(dest => dest.IsSent, src => src.MapFrom(x => x.IsSent))
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId));
        
        CreateMap<CompanyNotification, CompanyNotificationDTO>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body))
            .ForMember(dest => dest.CreatedAt, src => src.MapFrom(x => x.CreatedAt))
            .ForMember(dest => dest.IsRead, src => src.MapFrom(x => x.IsRead))
            .ForMember(dest => dest.IsSent, src => src.MapFrom(x => x.IsSent))
            .ForMember(dest => dest.CompanyId, src => src.MapFrom(x => x.CompanyId));

        CreateMap<AdminNotification, AdminNotificationDTO>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body))
            .ForMember(dest => dest.CreatedAt, src => src.MapFrom(x => x.CreatedAt))
            .ForMember(dest => dest.IsRead, src => src.MapFrom(x => x.IsRead))
            .ForMember(dest => dest.IsSent, src => src.MapFrom(x => x.IsSent))
            .ForMember(dest => dest.AdminId, src => src.MapFrom(x => x.AdminId))
            .ForMember(dest => dest.IsResolved, src => src.MapFrom(x => x.IsResolved))
            .ForMember(dest => dest.Brand, src => src.MapFrom(x => x.Brand))
            .ForMember(dest => dest.Model, src => src.MapFrom(x => x.Model))
            .ForMember(dest => dest.Year, src => src.MapFrom(x => x.Year));
    }
}