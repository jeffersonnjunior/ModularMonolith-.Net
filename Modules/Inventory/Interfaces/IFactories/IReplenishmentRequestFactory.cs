using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Dtos.ReplenishmentRequestDtos;
using Modules.Inventory.Entities;

namespace Modules.Inventory.Interfaces.IFactories;

public interface IReplenishmentRequestFactory
{
    ReplenishmentRequest CreateReplenishmentRequest();
    ReplenishmentRequest MapToReplenishmentRequest(ReplenishmentRequestCreateDto dto);
    void MapToReplenishmentRequestFromUpdateDto(ReplenishmentRequest entity, ReplenishmentRequestUpdateDto dto);
    ReplenishmentRequestReadDto MapToReplenishmentRequestReadDto(ReplenishmentRequest entity);
}
