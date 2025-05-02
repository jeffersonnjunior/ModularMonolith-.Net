using Common.Exceptions;
using Common.ICache.Services;
using Modules.Inventory.Dtos.ReplenishmentRequestDtos;
using Modules.Inventory.Entities;
using Modules.Inventory.Enums;
using Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;
using Modules.Inventory.Interfaces.IDecorators;
using Modules.Inventory.Interfaces.IFactories;
using Modules.Inventory.Interfaces.IQuerys.IReplenishmentRequestQuerys;

namespace Modules.Inventory.Decorators;

public class ReplenishmentRequestDecorator : IReplenishmentRequestDecorator
{
    private readonly IReplenishmentRequestGetByElement _getByElement;
    private readonly IReplenishmentRequestGetFilter _getFilter;
    private readonly IReplenishmentRequestCreateCommand _createCommand;
    private readonly IReplenishmentRequestUpdateCommand _updateCommand;
    private readonly IReplenishmentRequestDeleteCommand _deleteCommand;
    private readonly IReplenishmentRequestFactory _replenishmentRequestFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;

    public ReplenishmentRequestDecorator(
        IReplenishmentRequestGetByElement getByElement,
        IReplenishmentRequestGetFilter getFilter,
        IReplenishmentRequestCreateCommand createCommand,
        IReplenishmentRequestUpdateCommand updateCommand,
        IReplenishmentRequestDeleteCommand deleteCommand,
        IReplenishmentRequestFactory replenishmentRequestFactory,
        ICacheService cacheService,
        NotificationContext notificationContext
        )
    {
        _getByElement = getByElement;
        _getFilter = getFilter;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _replenishmentRequestFactory = replenishmentRequestFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
    }

    public ReplenishmentRequestReadDto GetById(Guid id)
    {
        var request = ValidateRequestIds(id);

        if (request is null) return null;

        string cacheKey = $"ReplenishmentRequest:{id}";

        var cachedRequest = _cacheService.Get<ReplenishmentRequestReadDto>(cacheKey);
        if (cachedRequest != null) return cachedRequest;

        ReplenishmentRequestReadDto requestReadDto = _replenishmentRequestFactory.MapToReplenishmentRequestReadDto(request);

        _cacheService.Set(cacheKey, request, TimeSpan.FromHours(1));

        return requestReadDto;
    }

    public ReplenishmentRequestReturnFilterDto GetFilter(ReplenishmentRequestGetFilterDto filter)
    {
        string cacheKey = $"ReplenishmentRequestFilter:{filter.PartCodeContains}:{filter.RequestedQuantityEqual}:{filter.ReplenishmentStatusEqual}:{filter.RequestedAtEqual}:{filter.PageSize}:{filter.PageNumber}";

        var cachedResult = _cacheService.Get<ReplenishmentRequestReturnFilterDto>(cacheKey);

        if (cachedResult != null) return cachedResult;

        var request = _getFilter.GetFilter(filter);

        _cacheService.Set(cacheKey, request, TimeSpan.FromHours(1));

        return request;
    }

    public ReplenishmentRequestReadDto Create(ReplenishmentRequestCreateDto requestCreateDto)
    {
        if (!ValidateCommonRequestFields(requestCreateDto)) return null;

        var request = _replenishmentRequestFactory.MapToReplenishmentRequest(requestCreateDto);

        request = _createCommand.Add(request);

        return _replenishmentRequestFactory.MapToReplenishmentRequestReadDto(request);
    }

    public void Update(ReplenishmentRequestUpdateDto requestUpdateDto)
    {
        var request = ValidateRequestIds(requestUpdateDto.Id);
        if (request is null) return;

        if (!ValidateCommonRequestFields(requestUpdateDto)) return;

        _replenishmentRequestFactory.MapToReplenishmentRequestFromUpdateDto(request, requestUpdateDto);

        _updateCommand.Update(request);

        _cacheService.Remove($"ReplenishmentRequest:{requestUpdateDto.Id}");

        _cacheService.RemoveByPrefix("ReplenishmentRequestFilter:");
    }

    public void Delete(Guid id)
    {
        var request = ValidateRequestIds(id);

        if (request is null) return;

        _deleteCommand.Delete(request);

        _cacheService.Remove($"ReplenishmentRequest:{id}");

        _cacheService.RemoveByPrefix("ReplenishmentRequestFilter:");
    }

    private ReplenishmentRequest? ValidateRequestIds(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return null;
        }

        var request = _getByElement.GetById(id);

        if (request is null) return null;

        return request;
    }

    private bool ValidateCommonRequestFields(dynamic requestDto)
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(requestDto.PartCode))
        {
            _notificationContext.AddNotification("O campo 'Código da Peça' não pode estar vazio.");
            isValid = false;
        }

        if (requestDto.RequestedQuantity <= 0)
        {
            _notificationContext.AddNotification("O campo 'Quantidade Solicitada' deve ser maior que zero.");
            isValid = false;
        }

        if (!Enum.IsDefined(typeof(ReplenishmentStatus), requestDto.ReplenishmentStatus))
        {
            _notificationContext.AddNotification("O campo 'Status' deve ser válido.");
            isValid = false;
        }

        if (requestDto.RequestedAt == default(DateTime))
        {
            _notificationContext.AddNotification("O campo 'Data de Solicitação' deve ser uma data válida.");
            isValid = false;
        }

        return isValid;
    }
}