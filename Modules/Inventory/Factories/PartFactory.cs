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

    public PartCreateDto CreatePartCreateDto()
    {
        return new PartCreateDto
        {
            Code = string.Empty,
            Description = string.Empty,
            QuantityInStock = 0,
            MinimumRequired = 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    public PartReadDto CreatePartReadDto()
    {
        return new PartReadDto
        {
            Id = Guid.NewGuid(),
            Code = string.Empty,
            Description = string.Empty,
            QuantityInStock = 0,
            MinimumRequired = 0,
            CreatedAt = DateTime.UtcNow
        };
    }

    public PartUpdateDto CreatePartUpdateDto()
    {
        return new PartUpdateDto
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
            Id = Guid.NewGuid(),
            Code = dto.Code,
            Description = dto.Description,
            QuantityInStock = dto.QuantityInStock,
            MinimumRequired = dto.MinimumRequired,
            CreatedAt = dto.CreatedAt
        };
    }

    public PartCreateDto MapToPartCreateDto(Part entity)
    {
        return new PartCreateDto
        {
            Code = entity.Code,
            Description = entity.Description,
            QuantityInStock = entity.QuantityInStock,
            MinimumRequired = entity.MinimumRequired,
            CreatedAt = entity.CreatedAt
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

    public PartUpdateDto MapToPartUpdateDto(Part entity)
    {
        return new PartUpdateDto
        {
            Id = entity.Id,
            Code = entity.Code,
            Description = entity.Description,
            QuantityInStock = entity.QuantityInStock,
            MinimumRequired = entity.MinimumRequired,
            CreatedAt = entity.CreatedAt
        };
    }

    public Part MapToPartFromUpdateDto(PartUpdateDto dto)
    {
        return new Part
        {
            Id = dto.Id,
            Code = dto.Code,
            Description = dto.Description,
            QuantityInStock = dto.QuantityInStock,
            MinimumRequired = dto.MinimumRequired,
            CreatedAt = dto.CreatedAt
        };
    }
}