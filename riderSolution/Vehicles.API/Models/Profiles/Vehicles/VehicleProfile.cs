using AutoMapper;
using Vehicles.API.Models.DTOs.Vehicles;
using Vehicles.API.Models.Requests.Vehicles;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Models.Profiles.Vehicles;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<CreateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.Brand, src => src.MapFrom(x => x.Brand))
            .ForMember(dest => dest.Model, src => src.MapFrom(x => x.Model))
            .ForMember(dest => dest.TransmissionType, src => src.MapFrom(x => x.TransmissionType))
            .ForMember(dest => dest.FuelType, src => src.MapFrom(x => x.FuelType))
            .ForMember(dest => dest.Color, src => src.MapFrom(x => x.Color))
            .ForMember(dest => dest.Year, src => src.MapFrom(x => x.Year))
            .ForMember(dest => dest.EnginePower, src => src.MapFrom(x => x.EnginePower))
            .ForMember(dest => dest.Mileage, src => src.MapFrom(x => x.Mileage))
            .ForMember(dest => dest.MaxSpeed, src => src.MapFrom(x => x.MaxSpeed))
            .ForMember(dest => dest.EngineVolume, src => src.MapFrom(x => x.EngineVolume))
            .ForMember(dest => dest.FuelConsumption, src => src.MapFrom(x => x.FuelConsumption));

        CreateMap<UpdateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            .ForMember(dest => dest.Brand, src => src.MapFrom(x => x.Brand))
            .ForMember(dest => dest.Model, src => src.MapFrom(x => x.Model))
            .ForMember(dest => dest.TransmissionType, src => src.MapFrom(x => x.TransmissionType))
            .ForMember(dest => dest.FuelType, src => src.MapFrom(x => x.FuelType))
            .ForMember(dest => dest.Color, src => src.MapFrom(x => x.Color))
            .ForMember(dest => dest.Year, src => src.MapFrom(x => x.Year))
            .ForMember(dest => dest.EnginePower, src => src.MapFrom(x => x.EnginePower))
            .ForMember(dest => dest.Mileage, src => src.MapFrom(x => x.Mileage))
            .ForMember(dest => dest.MaxSpeed, src => src.MapFrom(x => x.MaxSpeed))
            .ForMember(dest => dest.EngineVolume, src => src.MapFrom(x => x.EngineVolume))
            .ForMember(dest => dest.FuelConsumption, src => src.MapFrom(x => x.FuelConsumption));

        CreateMap<Vehicle, VehicleDTO>()
            .ForMember(dest => dest.Brand, src => src.MapFrom(x => x.Brand))
            .ForMember(dest => dest.Model, src => src.MapFrom(x => x.Model))
            .ForMember(dest => dest.TransmissionType, src => src.MapFrom(x => x.TransmissionType))
            .ForMember(dest => dest.FuelType, src => src.MapFrom(x => x.FuelType))
            .ForMember(dest => dest.Color, src => src.MapFrom(x => x.Color))
            .ForMember(dest => dest.Year, src => src.MapFrom(x => x.Year))
            .ForMember(dest => dest.EnginePower, src => src.MapFrom(x => x.EnginePower))
            .ForMember(dest => dest.Mileage, src => src.MapFrom(x => x.Mileage))
            .ForMember(dest => dest.MaxSpeed, src => src.MapFrom(x => x.MaxSpeed))
            .ForMember(dest => dest.EngineVolume, src => src.MapFrom(x => x.EngineVolume))
            .ForMember(dest => dest.FuelConsumption, src => src.MapFrom(x => x.FuelConsumption));
    }
}
