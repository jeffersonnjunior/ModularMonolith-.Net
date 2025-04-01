using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Query;

namespace Modules.LogisticsDistributionModule.Interfaces.IQuery;

public interface IGetByIdDistributorQuery
{
    Task<DistributorReadDto> GetByIdAsync(DistributorGetByIdDto query);
}