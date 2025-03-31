using Common.IPersistence;
using Microsoft.EntityFrameworkCore;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Entities;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;
using System.Threading.Tasks;
using Common.IException;

namespace Modules.LogisticsDistributionModule.Queries.Implementations;

public class GetByIdDistributorQuery : IGetByIdDistributorQuery
{
    private readonly IBaseRepository _repository;
    private readonly INotificationContext _notificationContext;

    public GetByIdDistributorQuery(IBaseRepository repository, INotificationContext notificationContext)
    {
        _repository = repository;
        _notificationContext = notificationContext;
    }

    public async Task<DistributorReadDto> GetByIdAsync(DistributorGetByIdDto query)
    {
        var distributor = await _repository.QueryWithIncludes<Distributor>()
            .Where(d => d.Id == query.Id)
            .FirstOrDefaultAsync();

        if (distributor is null)
        {
            _notificationContext.AddNotification("Distribuição não existe.");
            return null;
        }

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