using Modules.Sales.Dtos.SaleDetailDtos;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.IFactories;

namespace Modules.Sales.Factories;

public class SaleDetailFactory : ISaleDetailFactory
{
    public SaleDetail CreateSaleDetail()
    {
        return new SaleDetail
        {
            Id = Guid.NewGuid(),
            CarSaleId = Guid.Empty,
            BuyerName = string.Empty,
            Price = 0,
            Discount = 0,
            PaymentMethod = string.Empty,
            Notes = string.Empty,
        };
    }

    public SaleDetail MapToSaleDetail(SaleDetailCreateDto dto)
    {
        return new SaleDetail
        {
            Id = Guid.NewGuid(),
            CarSaleId = dto.CarSaleId,
            BuyerName = dto.BuyerName,
            Price = dto.Price,
            Discount = dto.Discount,
            PaymentMethod = dto.PaymentMethod,
            Notes = dto.Notes,
        };
    }

    public void MapToSaleDetailFromUpdateDto(SaleDetail entity, SaleDetailUpdateDto dto)
    {
        entity.CarSaleId = dto.CarSaleId;
        entity.BuyerName = dto.BuyerName;
        entity.Price = dto.Price;
        entity.Discount = dto.Discount;
        entity.PaymentMethod = dto.PaymentMethod;
        entity.Notes = dto.Notes;
    }

    public SaleDetailReadDto MapToSaleDetailReadDto(SaleDetail entity)
    {
        return new SaleDetailReadDto
        {
            Id = entity.Id,
            CarSaleId = entity.CarSaleId,
            BuyerName = entity.BuyerName,
            Price = entity.Price,
            Discount = entity.Discount,
            PaymentMethod = entity.PaymentMethod,
            Notes = entity.Notes
        };
    }
}