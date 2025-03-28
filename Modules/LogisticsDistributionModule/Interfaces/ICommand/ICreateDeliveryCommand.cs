using Modules.LogisticsDistributionModule.Dtos;

namespace Modules.LogisticsDistributionModule.Interfaces;

public interface ICreateDeliveryCommand
{
    DeliveryCreateDto Add(DeliveryCreateDto dto);
}