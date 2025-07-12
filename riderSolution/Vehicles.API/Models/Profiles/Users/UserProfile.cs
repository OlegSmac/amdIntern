using AutoMapper;
using Vehicles.API.Contracts.DTOs.Users;
using Vehicles.API.Contracts.Requests.Users;
using Vehicles.Application.Auth.Requests;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Models.Profiles.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterRequest, User>()
            .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
            .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName));
        
        CreateMap<UpdateUserRequest, User>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
            .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName));
        
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
            .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName))
            .ForMember(dest => dest.Email, src => src.MapFrom(x => x.ApplicationUser.Email))
            .ForMember(dest => dest.Phone, src => src.MapFrom(x => x.ApplicationUser.PhoneNumber));
    }
}