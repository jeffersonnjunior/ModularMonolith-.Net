using AutoMapper;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Entities;

namespace Modules.LogisticsDistributionModule.Profiles;

public class ProfileConfigurationMap : Profile
{
    public ProfileConfigurationMap()
    {
        CreateMap<Distributor, DistributorCreateDto>().ReverseMap();
        CreateMap<Distributor, DistributorReadDto>().ReverseMap();
        CreateMap<Distributor, DistributorUpdateDto>().ReverseMap();
    }
}