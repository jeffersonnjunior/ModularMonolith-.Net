using Common.IPersistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modules.Sales.Dtos.SaleDetailDtos;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;

namespace Modules.Sales.Querys.SaleDetailQuerys;

public class SaleDetailGetFilter : ISaleDetailGetFilter
{
    private readonly IBaseRepository _repository;

    public SaleDetailGetFilter(IBaseRepository repository)
    {
        _repository = repository;
    }

    public SaleDetailReturnFilterDto GetFilter(SaleDetailGetFilterDto filter)
    {
        IQueryable<SaleDetail> query = _repository.Query<SaleDetail>();

        if (filter.CarSaleIdEqual != Guid.Empty)
            query = query.Where(sd => sd.CarSaleId == filter.CarSaleIdEqual);

        if (!string.IsNullOrWhiteSpace(filter.BuyerNameContains))
            query = query.Where(sd => sd.BuyerName.Contains(filter.BuyerNameContains));

        if (filter.PriceContainsEqual > 0)
            query = query.Where(sd => sd.Price == filter.PriceContainsEqual);

        if (filter.DiscountEqual > 0)
            query = query.Where(sd => sd.Discount == filter.DiscountEqual);

        if (!string.IsNullOrWhiteSpace(filter.PaymentMethodContains))
            query = query.Where(sd => sd.PaymentMethod.Contains(filter.PaymentMethodContains));

        if (!string.IsNullOrWhiteSpace(filter.NotesContains))
            query = query.Where(sd => sd.Notes.Contains(filter.NotesContains));

        int totalRegister = query.Count();

        var saleDetails = query
            .OrderByDescending(sd => sd.Price)
            .ThenBy(sd => sd.BuyerName)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(sd => new SaleDetailReadDto
            {
                Id = sd.Id,
                CarSaleId = sd.CarSaleId,
                BuyerName = sd.BuyerName,
                Price = sd.Price,
                Discount = sd.Discount,
                PaymentMethod = sd.PaymentMethod,
                Notes = sd.Notes
            })
            .ToList();

        return new SaleDetailReturnFilterDto
        {
            TotalRegister = totalRegister,
            TotalRegisterFilter = saleDetails.Count,
            TotalPages = (int)Math.Ceiling(totalRegister / (double)filter.PageSize),
            SaleDetailReadDto = saleDetails
        };
    }
}
