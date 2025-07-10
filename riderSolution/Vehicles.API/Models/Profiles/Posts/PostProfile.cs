using AutoMapper;
using Vehicles.API.Models.DTOs.Posts;
using Vehicles.API.Models.Requests.Posts;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.API.Models.Profiles.Posts;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<CreatePostRequest, Post>()
            .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body))
            .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.Price))
            .ForMember(dest => dest.CompanyId, src => src.MapFrom(x => x.CompanyId))
            .ForMember(dest => dest.VehicleId, src => src.MapFrom(x => x.VehicleId))
            .ForMember(dest => dest.Vehicle, src => src.Ignore()) 
            .ForMember(dest => dest.Categories, src => src.Ignore())
            .ForMember(dest => dest.Images, src => src.MapFrom(x => x.Images.Select(img => new PostImage { Url = img }).ToList()));
        
        CreateMap<UpdatePostRequest, Post>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body))
            .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.Price))
            .ForMember(dest => dest.Views, src => src.MapFrom(x => x.Views))
            .ForMember(dest => dest.IsHidden, src => src.MapFrom(x => x.IsHidden))
            .ForMember(dest => dest.CompanyId, src => src.MapFrom(x => x.CompanyId))
            .ForMember(dest => dest.VehicleId, src => src.MapFrom(x => x.VehicleId))
            .ForMember(dest => dest.Vehicle, src => src.Ignore())
            .ForMember(dest => dest.Categories, src => src.Ignore())
            .ForMember(dest => dest.Images, src => src.MapFrom(x => x.Images.Select(img => new PostImage { Url = img }).ToList()));

        CreateMap<Post, PostDTO>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Title, src => src.MapFrom(x => x.Title))
            .ForMember(dest => dest.Body, src => src.MapFrom(x => x.Body))
            .ForMember(dest => dest.Date, src => src.MapFrom(x => x.Date))
            .ForMember(dest => dest.Views, src => src.MapFrom(x => x.Views))
            .ForMember(dest => dest.IsHidden, src => src.MapFrom(x => x.IsHidden))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.Price))
            .ForMember(dest => dest.Images, src => src.MapFrom<PostImageUrlResolver>())
            .ForMember(dest => dest.Company, src => src.MapFrom(x => x.Company))
            .ForMember(dest => dest.Vehicle, src => src.MapFrom(x => x.Vehicle))
            .ForMember(dest => dest.Categories, src => src.MapFrom(x => x.Categories.Select(c => c.Name).ToList()));

    }
}