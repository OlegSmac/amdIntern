using AutoMapper;
using Vehicles.API.Models.DTOs.Vehicles;
using Vehicles.API.Models.Requests.Vehicles;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Models.Profiles.Vehicles;

public class MotorcycleProfile : Profile
{
    public MotorcycleProfile()
    {
        CreateMap<CreateVehicleRequest, Motorcycle>()
            .IncludeBase<CreateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.HasSidecar, src => src.MapFrom(x => x.MotorcycleInfo.HasSidecar));

        CreateMap<UpdateVehicleRequest, Motorcycle>()
            .IncludeBase<UpdateVehicleRequest, Vehicle>()
            .ForMember(dest => dest.HasSidecar, src => src.MapFrom(x => x.MotorcycleInfo.HasSidecar));

        CreateMap<Motorcycle, MotorcycleDTO>()
            .IncludeBase<Vehicle, VehicleDTO>()
            .ForMember(dest => dest.HasSidecar, src => src.MapFrom(x => x.HasSidecar));
    }
}
