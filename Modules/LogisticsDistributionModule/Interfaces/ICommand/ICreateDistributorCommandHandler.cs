using Modules.LogisticsDistributionModule.Dtos;

namespace Modules.LogisticsDistributionModule.Interfaces;

public interface ICreateDistributorCommandHandler
{
    DistributorCreateDto Add(DistributorCreateDto dto);
}