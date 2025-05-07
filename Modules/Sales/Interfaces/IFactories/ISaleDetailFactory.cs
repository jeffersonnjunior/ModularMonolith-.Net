using Modules.Sales.Dtos.SaleDetailDtos;
using Modules.Sales.Entities;

namespace Modules.Sales.Interfaces.IFactories;

public interface ISaleDetailFactory
{
    SaleDetail CreateSaleDetail();
    SaleDetail MapToSaleDetail(SaleDetailCreateDto dto);
    void MapToSaleDetailFromUpdateDto(SaleDetail entity, SaleDetailUpdateDto dto);
    SaleDetailReadDto MapToSaleDetailReadDto(SaleDetail entity);
}