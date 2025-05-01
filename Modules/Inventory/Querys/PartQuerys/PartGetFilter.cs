using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

namespace Modules.Inventory.Querys.PartQuerys;

public class PartGetFilter : IPartGetFilter
{
    private readonly IBaseRepository _repository;

    public PartGetFilter(IBaseRepository repository)
    {
        _repository = repository;
    }

    public PartReturnFilterDto GetFilter(PartGetFilterDto filter)
    {
        var query = _repository.Query<Part>();

        if (!string.IsNullOrEmpty(filter.CodeContains))
            query = query.Where(p => p.Code.Contains(filter.CodeContains));

        if (!string.IsNullOrEmpty(filter.DescriptionContains))
            query = query.Where(p => p.Description.Contains(filter.DescriptionContains));

        if (filter.QuantityInStockEqual > 0)
            query = query.Where(p => p.QuantityInStock == filter.QuantityInStockEqual);

        if (filter.MinimumRequiredEqual > 0)
            query = query.Where(p => p.MinimumRequired == filter.MinimumRequiredEqual);

        if (filter.CreatedAtEqual != default)
            query = query.Where(p => p.CreatedAt.Date == filter.CreatedAtEqual.Date);

        int totalRegister = query.Count();

        var parts = query
            .OrderBy(p => p.Code)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(p => new PartReadDto
            {
                Id = p.Id,
                Code = p.Code,
                Description = p.Description,
                QuantityInStock = p.QuantityInStock,
                MinimumRequired = p.MinimumRequired,
                CreatedAt = p.CreatedAt
            })
            .ToList();

        return new PartReturnFilterDto
        {
            TotalRegister = totalRegister,
            TotalRegisterFilter = parts.Count,
            TotalPages = (int)Math.Ceiling(totalRegister / (double)filter.PageSize),
            Parts = parts
        };
    }
}