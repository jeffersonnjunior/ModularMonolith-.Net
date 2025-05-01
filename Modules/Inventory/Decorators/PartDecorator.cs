using Common.Exceptions;
using Common.ICache.Services;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.IDecorators;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;

namespace Modules.Inventory.Decorators;

public class PartDecorator : IPartDecorator
{
    private readonly IPartCreateCommand _createCommand;
    private readonly IPartUpdateCommand _updateCommand;
    private readonly IPartGetByElement _getByElement;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;

    public PartDecorator(
        IPartCreateCommand createCommand,
        IPartUpdateCommand updateCommand,
        IPartGetByElement getByElement,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _getByElement = getByElement;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
    }

    public PartReadDto Create(PartCreateDto partCreateDto)
    {
        if (!ValidateCommonPartFields(partCreateDto)) return null;

        return _createCommand.Add(partCreateDto);
    }


    public void Update(PartUpdateDto partUpdateDto)
    {
        var part = ValidatePartIds(partUpdateDto.Id);
        if (part is null) return;

        if (!ValidateCommonPartFields(partUpdateDto)) return;

        _updateCommand.Update(partUpdateDto);

        _cacheService.Remove($"Part:{partUpdateDto.Id}");
    }

    public PartReadDto GetById(Guid id)
    {
        var part = ValidatePartIds(id);
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

    private PartReadDto? ValidatePartIds(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return null;
        }

        string cacheKey = $"Part:{id}";

        var cachedPart = _cacheService.Get<PartReadDto>(cacheKey);
        if (cachedPart != null)
        {
            return cachedPart;
        }

        var part = _getByElement.GetById(id);

        if (part is null) return null;

        _cacheService.Set(cacheKey, part, TimeSpan.FromHours(1));

        return part;
    }
}