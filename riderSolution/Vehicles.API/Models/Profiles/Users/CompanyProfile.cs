using AutoMapper;
using Vehicles.API.DTOs.Responses;
using Vehicles.API.Models.Requests.Users;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Models.Profiles.Users;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CreateCompanyRequest, Company>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password))
            .ForMember(dest => dest.Description,
            src => src.MapFrom(x => x.Description));
        
        CreateMap<UpdateCompanyRequest, Company>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password))
            .ForMember(dest => dest.Description,
                src => src.MapFrom(x => x.Description));
        
        CreateMap<Company, CompanyDTO>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Description,
                src => src.MapFrom(x => x.Description));
    }
}