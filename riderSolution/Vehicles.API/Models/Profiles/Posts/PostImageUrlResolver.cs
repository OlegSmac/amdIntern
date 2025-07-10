using AutoMapper;
using Vehicles.API.Models.DTOs;
using Vehicles.API.Models.DTOs.Posts;
using Vehicles.API.Options;
using Vehicles.API.Services;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.API.Models.Profiles.Posts;

public class PostImageUrlResolver : IValueResolver<Post, PostDTO, List<PostImageDTO>>
{
    private readonly MinioService _minioService;

    public PostImageUrlResolver(MinioService minioService) 
    {
        _minioService = minioService;
    }

    public List<PostImageDTO> Resolve(Post source, PostDTO destination, List<PostImageDTO> destMember, ResolutionContext context)
    {
        return source.Images.Select(img => new PostImageDTO
        {
            Url = _minioService.GetPresignedUrlAsync(img.Url).GetAwaiter().GetResult(),
        }).ToList();
    }
}
