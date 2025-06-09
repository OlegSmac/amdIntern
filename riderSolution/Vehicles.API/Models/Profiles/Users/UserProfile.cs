using AutoMapper;
using Vehicles.API.DTOs.Responses;
using Vehicles.API.Models.Requests.Users;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Models.Profiles.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password));
        
        CreateMap<UpdateUserRequest, User>()
            .ForMember(dest => dest.Id,
                src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email))
            .ForMember(dest => dest.Password,
                src => src.MapFrom(x => x.Password));
        
        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Name,
                src => src.MapFrom(x => x.Name))
            .ForMember(dest => dest.Email,
                src => src.MapFrom(x => x.Email));
    }
}