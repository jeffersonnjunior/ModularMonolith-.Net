using Modules.Inventory;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.IFactories;

namespace Modules.Inventory.Factories;

public class PartFactory : IPartFactory
{
    public Part CreatePart()
    {
        return new Part
        {
            Id = Guid.NewGuid(),
            Code = string.Empty,
            Description = string.Empty,
            QuantityInStock = 0,
            MinimumRequired = 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    public Part MapToPart(PartCreateDto dto)
    {
        return new Part
        {
            Id = Guid.Empty,
            Code = dto.Code,
            Description = dto.Description,
            QuantityInStock = dto.QuantityInStock,
            MinimumRequired = dto.MinimumRequired,
            CreatedAt = dto.CreatedAt
        };
    }

    public PartReadDto MapToPartReadDto(Part entity)
    {
        return new PartReadDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Description = entity.Description,
            QuantityInStock = entity.QuantityInStock,
            MinimumRequired = entity.MinimumRequired,
            CreatedAt = entity.CreatedAt
        };
    }
    public void MapToPartFromUpdateDto(Part entity, PartUpdateDto dto)
    {
        entity.Code = dto.Code;
        entity.Description = dto.Description;
        entity.QuantityInStock = dto.QuantityInStock;
        entity.MinimumRequired = dto.MinimumRequired;
    }
}