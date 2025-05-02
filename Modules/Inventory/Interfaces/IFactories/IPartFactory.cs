namespace Modules.Inventory.Interfaces.IFactories;

using Modules.Inventory;
using Modules.Inventory.Dtos.PartDtos;

public interface IPartFactory
{
    Part CreatePart();
    Part MapToPart(PartCreateDto dto);
    void MapToPartFromUpdateDto(Part entity, PartUpdateDto dto);
    PartReadDto MapToPartReadDto(Part entity);
}
