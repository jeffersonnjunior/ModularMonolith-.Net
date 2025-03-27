using Common.Data;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Entities;
using Modules.LogisticsDistributionModule.Interfaces;

namespace Modules.LogisticsDistributionModule.Command;

public class CreateDistributorCommandHandler : ICreateDistributorCommandHandler
{
    private readonly IBaseRepository _baseRepository;

    public CreateDistributorCommandHandler(IBaseRepository baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public DistributorCreateDto Add(DistributorCreateDto request)
    {
        var distributor = new Distributor
        {
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
        };

        _baseRepository.Add(distributor);
        _baseRepository.SaveChanges();

        return new DistributorCreateDto
        {
            Name = distributor.Name,
            Address = distributor.Address,
            Phone = distributor.Phone,
            Email = distributor.Email,
        };
    }
}