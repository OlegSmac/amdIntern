using AutoMapper;
using Vehicles.API.Contracts.DTOs.Vehicles;
using Vehicles.API.Contracts.Requests.Vehicles;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Models.Profiles.Vehicles;

public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CreateVehicleRequest, Car>()
            .IncludeBase<CreateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.BodyType, src => src.MapFrom(x => x.CarInfo.BodyType))
            .ForMember(dest => dest.Seats, src => src.MapFrom(x => x.CarInfo.Seats))
            .ForMember(dest => dest.Doors, src => src.MapFrom(x => x.CarInfo.Doors));

        CreateMap<UpdateVehicleRequest, Car>()
            .IncludeBase<UpdateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.BodyType, src => src.MapFrom(x => x.CarInfo.BodyType))
            .ForMember(dest => dest.Seats, src => src.MapFrom(x => x.CarInfo.Seats))
            .ForMember(dest => dest.Doors, src => src.MapFrom(x => x.CarInfo.Doors));

        CreateMap<Car, CarDTO>()
            .IncludeBase<Vehicle, VehicleDTO>()
            .ForMember(dest => dest.BodyType, src => src.MapFrom(x => x.BodyType))
            .ForMember(dest => dest.Seats, src => src.MapFrom(x => x.Seats))
            .ForMember(dest => dest.Doors, src => src.MapFrom(x => x.Doors));
            
    }
}