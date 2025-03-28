using Common.IPersistence;
using Microsoft.EntityFrameworkCore;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Entities;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;
using System.Threading.Tasks;

namespace Modules.LogisticsDistributionModule.Queries.Implementations;

public class GetByIdDistributorQuery : IGetByIdDistributorQuery
{
    private readonly IBaseRepository _repository;

    public GetByIdDistributorQuery(IBaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<DistributorReadDto> GetByIdAsync(DistributorGetByIdDto query)
    {
        var distributor = await _repository.QueryWithIncludes<Distributor>()
            .Where(d => d.Id == query.Id)
            .FirstOrDefaultAsync();

        if (distributor == null)
            return null;

        return new DistributorReadDto
        {
            Id = distributor.Id,
            Name = distributor.Name,
            Address = distributor.Address,
            Phone = distributor.Phone,
            Email = distributor.Email
        };
    }
}