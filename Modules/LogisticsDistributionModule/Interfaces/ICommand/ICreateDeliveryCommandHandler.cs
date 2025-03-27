using Modules.LogisticsDistributionModule.Dtos;

namespace Modules.LogisticsDistributionModule.Interfaces;

public interface ICreateDeliveryCommandHandler
{
    DeliveryCreateDto Add(DeliveryCreateDto dto);
}