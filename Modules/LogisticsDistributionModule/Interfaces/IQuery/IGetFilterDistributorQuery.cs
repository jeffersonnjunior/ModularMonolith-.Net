using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Dtos.FilterReturnDtos;

namespace Modules.LogisticsDistributionModule.Interfaces.IQuery;

public interface IGetFilterDistributorQuery
{
    Task<FilterReturn<DistributorReadDto>> GetFilteredDistributorsAsync(DistributorGetFilterDto filter);
}