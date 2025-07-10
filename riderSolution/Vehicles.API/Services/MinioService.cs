using Minio;
using Minio.DataModel.Args;
using Microsoft.Extensions.Options;
using Vehicles.API.Options;

namespace Vehicles.API.Services;

public class MinioService
{
    private readonly IMinioClient _minioClient;
    private readonly MinioSettings _settings;

    public MinioService(IOptions<MinioSettings> options)
    {
        _settings = options.Value;
        _minioClient = new MinioClient()
            .WithEndpoint(_settings.Host)
            .WithCredentials(_settings.AccessKey, _settings.SecretKey)
            .Build();
    }
    
    public async Task<string> UploadFileAsync(IFormFile file, string folder)
    {
        var objectName = $"{folder}/{Guid.NewGuid()}_{file.FileName}";

        await using var stream = file.OpenReadStream();

        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType));

        return objectName;
    }

    public async Task<string> GetPresignedUrlAsync(string objectName)
    {
        var url = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(_settings.Bucket)
            .WithObject(objectName)
            .WithExpiry(60 * 60));

        return url;
    }
}