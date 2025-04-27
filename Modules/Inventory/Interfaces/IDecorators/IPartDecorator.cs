using Modules.Inventory.Dtos.PartDtos;

namespace Modules.Inventory.Interfaces.IDecorators;

public interface IPartDecorator
{
    void Create(PartCreateDto partCreateDto);
    void Update(PartUpdateDto partUpdateDto);
}
