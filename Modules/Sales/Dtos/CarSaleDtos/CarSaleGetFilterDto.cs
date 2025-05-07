using Common.IPersistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Entities;
using Modules.Sales.Enums;
using Modules.Sales.Interfaces.IQuerys;

namespace Modules.Sales.Querys.CarSaleQuerys;

public class CarSaleGetFilter : ICarSaleGetFilter
{
    private readonly IBaseRepository _repository;

    public CarSaleGetFilter(IBaseRepository repository)
    {
        _repository = repository;
    }

    public CarSaleReturnFilterDto GetFilter(CarSaleGetFilterDto filter)
    {
        IQueryable<CarSale> query = _repository.Query<CarSale>()
            .Include(cs => cs.SaleDetail)
            .Include(cs => cs.ProductionOrder);

        if (filter.ProductionOrderIdEqual != Guid.Empty)
            query = query.Where(cs => cs.ProductionOrderId == filter.ProductionOrderIdEqual);

        if (filter.StatusEqual != SaleStatus.None)
            query = query.Where(cs => cs.Status == filter.StatusEqual);

        if (filter.SoldAtEqual.HasValue)
        {
            var soldDate = filter.SoldAtEqual.Value.Date;
            query = query.Where(cs => cs.SoldAt.HasValue &&
                                    cs.SoldAt.Value.Date == soldDate);
        }

        int totalRegister = query.Count();

        var sales = query
            .OrderBy(cs => cs.SoldAt ?? DateTime.MaxValue)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(cs => new CarSaleReadDto
            {
                Id = cs.Id,
                ProductionOrderId = cs.ProductionOrderId,
                Status = cs.Status,
                SoldAt = cs.SoldAt,
                SaleDetail = cs.SaleDetail,
                ProductionOrder = cs.ProductionOrder
            })
            .ToList();

        return new CarSaleReturnFilterDto
        {
            TotalRegister = totalRegister,
            TotalRegisterFilter = sales.Count,
            TotalPages = (int)Math.Ceiling(totalRegister / (double)filter.PageSize),
            CarSales = sales
        };
    }
}