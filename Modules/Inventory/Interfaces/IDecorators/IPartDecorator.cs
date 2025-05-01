using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.IDecorators;

public interface IPartDecorator
{
    PartReadDto Create(PartCreateDto partCreateDto);
    void Update(PartUpdateDto partUpdateDto);
    PartReadDto GetById(Guid id);
}
