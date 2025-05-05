using Common.IPersistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modules.Production.Dtos.ProductionOrderDtos;
using Modules.Production.Entities;
using Modules.Production.Interfaces.IQuerys.IProductionOrderQuerys;

namespace Modules.Production.Querys.ProductionOrderQuerys;

public class ProductionOrderGetFilter : IProductionOrderGetFilter
{
    private readonly IBaseRepository _repository;

    public ProductionOrderGetFilter(IBaseRepository repository)
    {
        _repository = repository;
    }

    public ProductionOrderReturnFilterDto GetFilter(ProductionOrderGetFilterDto filter)
    {
        IQueryable<ProductionOrder> query = _repository.Query<ProductionOrder>()
            .Include(po => po.Parts);

        if (!string.IsNullOrEmpty(filter.ModelContains))
            query = query.Where(po => po.Model.Contains(filter.ModelContains));

        query = query.Where(po => po.ProductionStatus == filter.ProductionStatusEqual);

        if (filter.CreatedAtEqual != default)
            query = query.Where(po => po.CreatedAt.Date == filter.CreatedAtEqual.Date);

        if (filter.CompletedAtEqual.HasValue)
            query = query.Where(po => po.CompletedAt.HasValue &&
                                    po.CompletedAt.Value.Date == filter.CompletedAtEqual.Value.Date);

        int totalRegister = query.Count();

        var orders = query
            .OrderBy(po => po.Model)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(po => new ProductionOrderReadDto
            {
                Id = po.Id,
                Model = po.Model,
                ProductionStatus = po.ProductionStatus,
                CreatedAt = po.CreatedAt,
                CompletedAt = po.CompletedAt,
                Parts = po.Parts
            })
            .ToList();

        return new ProductionOrderReturnFilterDto
        {
            TotalRegister = totalRegister,
            TotalRegisterFilter = orders.Count,
            TotalPages = (int)Math.Ceiling(totalRegister / (double)filter.PageSize),
            ProductionOrder = orders
        };
    }
}