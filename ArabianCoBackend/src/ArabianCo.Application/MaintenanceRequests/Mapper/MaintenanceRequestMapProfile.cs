using ArabianCo.Domain.MaintenanceRequests;
using ArabianCo.MaintenanceRequests.Dto;
using AutoMapper;

namespace ArabianCo.MaintenanceRequests.Mapper;

internal class MaintenanceRequestMapProfile:Profile
{
    public MaintenanceRequestMapProfile()
    {
        CreateMap<CreateMaintenanceRequestDto, MaintenanceRequest>();
        CreateMap<UpdateMaintenanceRequestDto, MaintenanceRequest>();
        CreateMap<MaintenanceRequest, LiteMaintenanceRequestDto>();
        CreateMap<MaintenanceRequest, MaintenanceRequestDto>()
            .ForMember(src=>src.Area, destinationMember => destinationMember.Ignore())
            ;
    }
}
