using Modules.Inventory.Dtos.ReplenishmentRequestDtos;

namespace Modules.Inventory.Interfaces.IDecorators;

public interface IReplenishmentRequestDecorator
{
    ReplenishmentRequestReadDto GetById(Guid id);
    ReplenishmentRequestReturnFilterDto GetFilter(ReplenishmentRequestGetFilterDto filter);
    ReplenishmentRequestReadDto Create(ReplenishmentRequestCreateDto requestCreateDto);
    void Update(ReplenishmentRequestUpdateDto requestUpdateDto);
    void Delete(Guid id);
}
