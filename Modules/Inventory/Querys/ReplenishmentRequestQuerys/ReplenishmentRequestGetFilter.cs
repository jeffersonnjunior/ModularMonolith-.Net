using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.ReplenishmentRequestDtos;
using Modules.Inventory.Entities;
using Modules.Inventory.Enums;
using Modules.Inventory.Interfaces.IQuerys.IReplenishmentRequestQuerys;

namespace Modules.Inventory.Querys.ReplenishmentRequestQuerys;

public class ReplenishmentRequestGetFilter : IReplenishmentRequestGetFilter
{
    private readonly IBaseRepository _repository;

    public ReplenishmentRequestGetFilter(IBaseRepository repository)
    {
        _repository = repository;
    }

    public ReplenishmentRequestReturnFilterDto GetFilter(ReplenishmentRequestGetFilterDto filter)
    {
        var query = _repository.Query<ReplenishmentRequest>();

        if (!string.IsNullOrEmpty(filter.PartCodeContains))
            query = query.Where(r => r.PartCode.Contains(filter.PartCodeContains));

        if (filter.RequestedQuantityEqual.HasValue && filter.RequestedQuantityEqual > 0)
            query = query.Where(r => r.RequestedQuantity == filter.RequestedQuantityEqual);

        if (filter.ReplenishmentStatusEqual.HasValue)
            query = query.Where(r => r.ReplenishmentStatus == filter.ReplenishmentStatusEqual);

        if (filter.RequestedAtEqual.HasValue && filter.RequestedAtEqual != default)
            query = query.Where(r => r.RequestedAt.Date == filter.RequestedAtEqual.Value.Date);

        int totalRegister = query.Count();

        var requests = query
            .OrderBy(r => r.PartCode)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(r => new ReplenishmentRequestReadDto
            {
                Id = r.Id,
                PartCode = r.PartCode,
                RequestedQuantity = r.RequestedQuantity,
                ReplenishmentStatus = r.ReplenishmentStatus,
                RequestedAt = r.RequestedAt
            })
            .ToList();

        return new ReplenishmentRequestReturnFilterDto
        {
            TotalRegister = totalRegister,
            TotalRegisterFilter = requests.Count,
            TotalPages = (int)Math.Ceiling(totalRegister / (double)filter.PageSize),
            ReplenishmentRequestReadDto = requests
        };
    }
}