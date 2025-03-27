using Common.Data;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Entities;
using System.Threading.Tasks;
using Modules.LogisticsDistributionModule.Interfaces;

namespace Modules.LogisticsDistributionModule.Command;

public class CreateDeliveryCommandHandler : ICreateDeliveryCommandHandler
{
    private readonly IBaseRepository _baseRepository;

    public CreateDeliveryCommandHandler(IBaseRepository baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public DeliveryCreateDto Add (DeliveryCreateDto request)
    {
        var delivery = new Delivery
        {
            VehicleId = request.VehicleId,
            DistributorId = request.DistributorId,
            DeliveryDate = request.DeliveryDate,
            DeliveryStatus = request.DeliveryStatus
        };

        _baseRepository.Add(delivery);
        _baseRepository.SaveChanges();

        return new DeliveryCreateDto
        {
            VehicleId = delivery.VehicleId,
            DistributorId = delivery.DistributorId,
            DeliveryDate = delivery.DeliveryDate,
            DeliveryStatus = delivery.DeliveryStatus
        };
    }
}