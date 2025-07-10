using AutoMapper;
using Vehicles.API.Models.DTOs.Users;
using Vehicles.API.Models.Requests.Users;
using Vehicles.Application.Auth.Requests;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Models.Profiles.Users;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<RegisterRequest, Company>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description));
        
        CreateMap<UpdateUserRequest, Company>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.CompanyName))
            .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description));
        
        CreateMap<Company, CompanyDTO>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
            .ForMember(dest => dest.Email, src => src.MapFrom(x => x.ApplicationUser.Email))
            .ForMember(dest => dest.Phone, src => src.MapFrom(x => x.ApplicationUser.PhoneNumber));
    }
}