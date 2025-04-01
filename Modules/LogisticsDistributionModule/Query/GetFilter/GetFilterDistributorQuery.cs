using System.Linq;
using System.Threading.Tasks;
using Common.IPersistence;
using Microsoft.EntityFrameworkCore;
using Modules.LogisticsDistributionModule.Dtos;
using Modules.LogisticsDistributionModule.Dtos.FilterReturnDtos;
using Modules.LogisticsDistributionModule.Entities;
using Modules.LogisticsDistributionModule.Interfaces;
using Modules.LogisticsDistributionModule.Interfaces.IQuery;

namespace Modules.LogisticsDistributionModule.Query.GetFilter;

public class GetFilterDistributorQuery : IGetFilterDistributorQuery
{
    private readonly IBaseRepository _repository;

    public GetFilterDistributorQuery(IBaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<FilterReturn<DistributorReadDto>> GetFilteredDistributorsAsync(DistributorGetFilterDto filter)
    {
        var query = _repository.QueryWithIncludes<Distributor>().AsQueryable();

        if (!string.IsNullOrEmpty(filter.NameContains)) query = query.Where(d => d.Name.Contains(filter.NameContains));
        if (!string.IsNullOrEmpty(filter.AddressContains)) query = query.Where(d => d.Address.Contains(filter.AddressContains));
        if (!string.IsNullOrEmpty(filter.PhoneContains)) query = query.Where(d => d.Phone.Contains(filter.PhoneContains));
        if (!string.IsNullOrEmpty(filter.EmailContains)) query = query.Where(d => d.Email.Contains(filter.EmailContains));

        var totalRegister = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalRegister / (double)filter.PageSize);

        var distributors = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var distributorDtos = distributors.Select(d => new DistributorReadDto
        {
            Id = d.Id,
            Name = d.Name,
            Address = d.Address,
            Phone = d.Phone,
            Email = d.Email
        }).ToList();

        return new FilterReturn<DistributorReadDto>
        {
            TotalRegister = totalRegister,
            TotalRegisterFilter = distributorDtos.Count,
            TotalPages = totalPages,
            ItensList = distributorDtos
        };
    }
}