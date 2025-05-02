using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.IDecorators;

public interface IPartDecorator
{
    PartReadDto GetById(Guid id);
    PartReturnFilterDto GetFilter(PartGetFilterDto filter);
    PartReadDto Create(PartCreateDto partCreateDto);
    void Update(PartUpdateDto partUpdateDto);
    void Delete(Guid id);
}
