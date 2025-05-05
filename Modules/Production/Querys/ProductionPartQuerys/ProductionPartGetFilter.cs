using Common.IPersistence.IRepositories;
using Microsoft.EntityFrameworkCore;
using Modules.Production.Dtos.ProductionPartDtos;
using Modules.Production.Entities;
using Modules.Production.Interfaces.IQuerys.IProductionPartQuerys;

namespace Modules.Production.Querys.ProductionPartQuerys;

public class ProductionPartGetFilter : IProductionPartGetFilter
{
    private readonly IBaseRepository _repository;

    public ProductionPartGetFilter(IBaseRepository repository)
    {
        _repository = repository;
    }

    public ProductionPartReturnFilterDto GetFilter(ProductionPartGetFilterDto filter)
    {
        IQueryable<ProductionPart> query = _repository.Query<ProductionPart>()
            .Include(pp => pp.ProductionOrder);

        if (filter.ProductionOrderIdEqual != Guid.Empty)
            query = query.Where(pp => pp.ProductionOrderId == filter.ProductionOrderIdEqual);

        if (!string.IsNullOrEmpty(filter.PartCodeContains))
            query = query.Where(pp => pp.PartCode.Contains(filter.PartCodeContains));

        if (filter.QuantityUsedEqual > 0)
            query = query.Where(pp => pp.QuantityUsed == filter.QuantityUsedEqual);

        int totalRegister = query.Count();

        var parts = query
            .OrderBy(pp => pp.PartCode)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(pp => new ProductionPartReadDto
            {
                Id = pp.Id,
                ProductionOrderId = pp.ProductionOrderId,
                PartCode = pp.PartCode,
                QuantityUsed = pp.QuantityUsed,
                ProductionOrder = pp.ProductionOrder
            })
            .ToList();

        return new ProductionPartReturnFilterDto
        {
            TotalRegister = totalRegister,
            TotalRegisterFilter = parts.Count,
            TotalPages = (int)Math.Ceiling(totalRegister / (double)filter.PageSize),
            ProductionPart = parts
        };
    }
}