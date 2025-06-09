using AutoMapper;
using Vehicles.API.DTOs.Responses;
using Vehicles.API.Models.Requests.Users;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Models.Profiles.Users;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<CreateAdminRequest, Admin>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password));
        
        CreateMap<UpdateAdminRequest, Admin>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password));
        
        CreateMap<Admin, AdminDTO>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email));
    }
}