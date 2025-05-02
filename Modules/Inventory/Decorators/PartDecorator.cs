using Common.Exceptions;
using Common.ICache.Services;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.IDecorators;
using Modules.Inventory.Interfaces.IFactories;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

namespace Modules.Inventory.Decorators;

public class PartDecorator : IPartDecorator
{
    private readonly IPartGetByElement _getByElement;
    private readonly IPartGetFilter _getFilter;
    private readonly IPartCreateCommand _createCommand;
    private readonly IPartUpdateCommand _updateCommand;
    private readonly IPartDeleteCommand _deleteCommand;
    private readonly IPartFactory _partFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;

    public PartDecorator(
        IPartGetByElement getByElement,
        IPartGetFilter getFilter,
        IPartCreateCommand createCommand,
        IPartUpdateCommand updateCommand,
        IPartDeleteCommand deleteCommand,
        IPartFactory partFactory,
        ICacheService cacheService,
        NotificationContext notificationContext
        )
    {
        _getByElement = getByElement;
        _getFilter = getFilter;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _partFactory = partFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
        _partFactory = partFactory;
    }

    public PartReadDto GetById(Guid id)
    {
        var part = ValidatePartIds(id);

        if (part is null) return null;

        string cacheKey = $"Part:{id}";

        var cachedPart = _cacheService.Get<PartReadDto>(cacheKey);
        if (cachedPart != null) return cachedPart;

        PartReadDto partReadDto = _partFactory.MapToPartReadDto(part);

        _cacheService.Set(cacheKey, part, TimeSpan.FromHours(1));

        return partReadDto;
    }
    public PartReturnFilterDto GetFilter(PartGetFilterDto filter)
    {
        string cacheKey = $"PartFilter:{filter.CodeContains}:{filter.DescriptionContains}:{filter.QuantityInStockEqual}:{filter.MinimumRequiredEqual}:{filter.CreatedAtEqual}:{filter.PageSize}:{filter.PageNumber}";

        var cachedResult = _cacheService.Get<PartReturnFilterDto>(cacheKey);

        if (cachedResult != null) return cachedResult;

        var part = _getFilter.GetFilter(filter);

        _cacheService.Set(cacheKey, part, TimeSpan.FromHours(1));

        return part;
    }

    public PartReadDto Create(PartCreateDto partCreateDto)
    {
        if (!ValidateCommonPartFields(partCreateDto)) return null;

        var part = _partFactory.MapToPart(partCreateDto);

        part = _createCommand.Add(part);

        return _partFactory.MapToPartReadDto(part);
    }

    public void Update(PartUpdateDto partUpdateDto)
    {
        var part = ValidatePartIds(partUpdateDto.Id);
        if (part is null) return;

        if (!ValidateCommonPartFields(partUpdateDto)) return;

        _partFactory.MapToPartFromUpdateDto(part, partUpdateDto);

        _updateCommand.Update(part);

        _cacheService.Remove($"Part:{partUpdateDto.Id}");

        _cacheService.RemoveByPrefix("PartFilter:");
    }

    public void Delete(Guid id)
    {
        var part = ValidatePartIds(id);

        if (part is null) return;

        _deleteCommand.Delete(part);

        _cacheService.Remove($"Part:{id}");

        _cacheService.RemoveByPrefix("PartFilter:");
    }

    private Part? ValidatePartIds(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return null;
        }

        var part = _getByElement.GetById(id);

        if (part is null) return null;

        return part;
    }

    private bool ValidateCommonPartFields(dynamic partDto)
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(partDto.Code))
        {
            _notificationContext.AddNotification("O campo 'Código' não pode estar vazio.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(partDto.Description))
        {
            _notificationContext.AddNotification("O campo 'Descrição' não pode estar vazio.");
            isValid = false;
        }

        if (partDto.QuantityInStock < 0)
        {
            _notificationContext.AddNotification("O campo 'Quantidade em Estoque' não pode ser negativo.");
            isValid = false;
        }

        if (partDto.MinimumRequired < 0)
        {
            _notificationContext.AddNotification("O campo 'Quantidade Mínima Necessária' não pode ser negativo.");
            isValid = false;
        }

        if (partDto.CreatedAt == default(DateTime))
        {
            _notificationContext.AddNotification("O campo 'Data de Criação' deve ser uma data válida.");
            isValid = false;
        }

        return isValid;
    }
}