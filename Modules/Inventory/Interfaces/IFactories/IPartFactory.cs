namespace Modules.Inventory.Interfaces.IFactories;

using Modules.Inventory;
using Modules.Inventory.Dtos.PartDtos;

public interface IPartFactory
{
    Part CreatePart();
    PartCreateDto CreatePartCreateDto();
    PartReadDto CreatePartReadDto();
    PartUpdateDto CreatePartUpdateDto();
    Part MapToPart(PartCreateDto dto);
    PartCreateDto MapToPartCreateDto(Part entity);
    PartReadDto MapToPartReadDto(Part entity);
    PartUpdateDto MapToPartUpdateDto(Part entity);
    Part MapToPartFromUpdateDto(PartUpdateDto dto);
}
