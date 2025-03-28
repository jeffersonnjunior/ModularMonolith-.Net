using Modules.LogisticsDistributionModule.Dtos;

namespace Modules.LogisticsDistributionModule.Interfaces;

public interface ICreateDistributorCommand
{
    DistributorCreateDto Add(DistributorCreateDto dto);
}