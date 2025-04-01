using System.Threading.Tasks;
using AutoMapper;
using Common.IPersistence;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Entities;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;
using Modules.LogisticsDistributionModule.Queries.Implementations;
using Common.IException;
using Modules.LogisticsDistributionModule.Interfaces.ICommand.IUpdate;

namespace Modules.LogisticsDistributionModule.Command.Update;

public class UpdateDistributorCommand : IUpdateDistributorCommand
{
    private readonly IBaseRepository _repository;
    private readonly IMapper _mapper;
    private readonly IGetByIdDistributorQuery _getByIdDistributorQuery;
    private readonly INotificationContext _notificationContext;

    public UpdateDistributorCommand(IBaseRepository repository, IMapper mapper, IGetByIdDistributorQuery getByIdDistributorQuery, INotificationContext notificationContext)
    {
        _repository = repository;
        _mapper = mapper;
        _getByIdDistributorQuery = getByIdDistributorQuery;
        _notificationContext = notificationContext;
    }

    public async Task<DistributorReadDto> UpdateDistributorAsync(DistributorUpdateDto updateDto)
    {
        var distributorDto = await _getByIdDistributorQuery.GetByIdAsync(new DistributorGetByIdDto { Id = updateDto.Id });

        if (distributorDto == null)
        {
            _notificationContext.AddNotification("Distribuidora não existe.");
            return null;
        }

        var distributor = _mapper.Map<Distributor>(distributorDto);
        _mapper.Map(updateDto, distributor);
        _repository.Update(distributor);

        return _mapper.Map<DistributorReadDto>(distributor);
    }
}