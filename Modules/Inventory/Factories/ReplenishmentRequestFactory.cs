using Modules.Inventory.Dtos.ReplenishmentRequestDtos;
using Modules.Inventory.Entities;
using Modules.Inventory.Enums;
using Modules.Inventory.Interfaces.IFactories;

namespace Modules.Inventory.Factories;

public class ReplenishmentRequestFactory : IReplenishmentRequestFactory
{
    public ReplenishmentRequest CreateReplenishmentRequest()
    {
        return new ReplenishmentRequest
        {
            Id = Guid.NewGuid(),
            PartCode = string.Empty,
            RequestedQuantity = 0,
            ReplenishmentStatus = ReplenishmentStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
    }

    public ReplenishmentRequest MapToReplenishmentRequest(ReplenishmentRequestCreateDto dto)
    {
        return new ReplenishmentRequest
        {
            Id = Guid.Empty,
            PartCode = dto.PartCode,
            RequestedQuantity = dto.RequestedQuantity,
            ReplenishmentStatus = dto.ReplenishmentStatus,
            RequestedAt = dto.RequestedAt
        };
    }

    public void MapToReplenishmentRequestFromUpdateDto(ReplenishmentRequest entity, ReplenishmentRequestUpdateDto dto)
    {
        entity.PartCode = dto.PartCode;
        entity.RequestedQuantity = dto.RequestedQuantity;
        entity.ReplenishmentStatus = dto.ReplenishmentStatus;
        entity.RequestedAt = dto.RequestedAt;
    }

    public ReplenishmentRequestReadDto MapToReplenishmentRequestReadDto(ReplenishmentRequest entity)
    {
        return new ReplenishmentRequestReadDto
        {
            Id = entity.Id,
            PartCode = entity.PartCode,
            RequestedQuantity = entity.RequestedQuantity,
            ReplenishmentStatus = entity.ReplenishmentStatus,
            RequestedAt = entity.RequestedAt
        };
    }
}