using System.Threading.Tasks;
using Common.IPersistence;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;
using Common.IException;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Entities;
using Modules.LogisticsDistributionModule.Interfaces.ICommand.IDelete;

namespace Modules.LogisticsDistributionModule.Command.Delete;

public class DeleteDistributorCommand : IDeleteDistributorCommand
{
    private readonly IBaseRepository _repository;
    private readonly IGetByIdDistributorQuery _getByIdDistributorQuery;
    private readonly INotificationContext _notificationContext;

    public DeleteDistributorCommand(IBaseRepository repository, IGetByIdDistributorQuery getByIdDistributorQuery, INotificationContext notificationContext)
    {
        _repository = repository;
        _getByIdDistributorQuery = getByIdDistributorQuery;
        _notificationContext = notificationContext;
    }

    public async Task<bool> DeleteDistributorAsync(Guid id)
    {
        var distributorDto = await _getByIdDistributorQuery.GetByIdAsync(new DistributorGetByIdDto { Id = id });

        if (distributorDto == null)
        {
            _notificationContext.AddNotification("Distribuidora não existe.");
            return false;
        }

        var distributor = new Distributor { Id = distributorDto.Id };
        _repository.Remove(distributor);

        return true;
    }
}