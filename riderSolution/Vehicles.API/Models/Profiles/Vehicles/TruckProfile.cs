using AutoMapper;
using Vehicles.API.Contracts.DTOs.Vehicles;
using Vehicles.API.Contracts.Requests.Vehicles;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Models.Profiles.Vehicles;

public class TruckProfile : Profile
{
    public TruckProfile()
    {
        CreateMap<CreateVehicleRequest, Truck>()
            .IncludeBase<CreateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.CabinType, src => src.MapFrom(x => x.TruckInfo.CabinType))
            .ForMember(dest => dest.LoadCapacity, src => src.MapFrom(x => x.TruckInfo.LoadCapacity));

        CreateMap<UpdateVehicleRequest, Truck>()
            .IncludeBase<UpdateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.CabinType, src => src.MapFrom(x => x.TruckInfo.CabinType))
            .ForMember(dest => dest.LoadCapacity, src => src.MapFrom(x => x.TruckInfo.LoadCapacity));

        CreateMap<Truck, TruckDTO>()
            .IncludeBase<Vehicle, VehicleDTO>()
            .ForMember(dest => dest.CabinType, src => src.MapFrom(x => x.CabinType))
            .ForMember(dest => dest.LoadCapacity, src => src.MapFrom(x => x.LoadCapacity));
    }
}
