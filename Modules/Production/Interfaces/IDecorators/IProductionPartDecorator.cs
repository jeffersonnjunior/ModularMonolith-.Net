using Modules.Production.Dtos.ProductionPartDtos;

namespace Modules.Production.Interfaces.IDecorators;

public interface IProductionPartDecorator
{
    ProductionPartReadDto GetById(Guid id);
    ProductionPartReturnFilterDto GetFilter(ProductionPartGetFilterDto filter);
    ProductionPartReadDto Create(ProductionPartCreateDto productionPartCreateDto);
    void Update(ProductionPartUpdateDto productionPartUpdateDto);
    void Delete(Guid id);
}
