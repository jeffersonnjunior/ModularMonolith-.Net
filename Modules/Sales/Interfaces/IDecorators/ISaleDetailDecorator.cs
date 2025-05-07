using Modules.Sales.Dtos.SaleDetailDtos;

namespace Modules.Sales.Interfaces.IDecorators;

public interface ISaleDetailDecorator
{
    SaleDetailReadDto GetById(Guid id);
    SaleDetailReturnFilterDto GetFilter(SaleDetailGetFilterDto filter);
    SaleDetailReadDto Create(SaleDetailCreateDto saleDetailCreateDto);
    void Update(SaleDetailUpdateDto saleDetailUpdateDto);
    void Delete(Guid id);
}