using Common.Exceptions;
using Common.ICache.Services;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.IFactories;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

namespace Modules.Inventory.Querys.PartQuerys;

public class PartGetByElement : IPartGetByElement
{
    private readonly IBaseRepository _repository;
    private readonly IPartFactory _partFactory;
    private readonly NotificationContext _notificationContext;
    private readonly ICacheService _cacheService;

    public PartGetByElement(
        IBaseRepository repository,
        IPartFactory partFactory,
        NotificationContext notificationContext,
        ICacheService cacheService)
    {
        _repository = repository;
        _partFactory = partFactory;
        _notificationContext = notificationContext;
        _cacheService = cacheService;
    }

    public PartReadDto GetById(Guid id)
    {
        string cacheKey = $"Part:{id}";

        var cachedPart = _cacheService.Get<PartReadDto>(cacheKey); 
        if (cachedPart != null)
        {
            return cachedPart;
        }

        Part part = _repository.Find<Part>(id);

        if (part == null)
        {
            _notificationContext.AddNotification("A peça com o ID especificado não foi encontrada.");
            return null;
        }

        var partDto = _partFactory.MapToPartReadDto(part);

        _cacheService.Set(cacheKey, partDto, TimeSpan.FromHours(1)); 

        return partDto;
    }
}
