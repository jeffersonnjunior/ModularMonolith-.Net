using Common.Exceptions;
using Common.ICache.Services;
using Modules.Production.Dtos.ProductionPartDtos;
using Modules.Production.Entities;
using Modules.Production.Interfaces.IDecorators;
using Modules.Production.Interfaces.IFactories;
using Modules.Production.Interfaces.IQuerys.IProductionPartQuerys;
using Modules.Production.Interfaces.ICommands.IProductionPartCommands;

namespace Modules.Production.Decorators;

public class ProductionPartDecorator : IProductionPartDecorator
{
    private readonly IProductionPartGetByElement _getByElement;
    private readonly IProductionPartGetFilter _getFilter;
    private readonly IProductionPartCreateCommand _createCommand;
    private readonly IProductionPartUpdateCommand _updateCommand;
    private readonly IProductionPartDeleteCommand _deleteCommand;
    private readonly IProductionPartFactory _productionPartFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;

    public ProductionPartDecorator(
        IProductionPartGetByElement getByElement,
        IProductionPartGetFilter getFilter,
        IProductionPartCreateCommand createCommand,
        IProductionPartUpdateCommand updateCommand,
        IProductionPartDeleteCommand deleteCommand,
        IProductionPartFactory productionPartFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _getByElement = getByElement;
        _getFilter = getFilter;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _productionPartFactory = productionPartFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
    }

    public ProductionPartReadDto GetById(Guid id)
    {
        var part = ValidateProductionPartIds(id);

        if (part is null) return null;

        string cacheKey = $"ProductionPart:{id}";

        var cachedPart = _cacheService.Get<ProductionPartReadDto>(cacheKey);
        if (cachedPart != null) return cachedPart;

        ProductionPartReadDto partReadDto = _productionPartFactory.MapToProductionOrderReadDto(part);

        _cacheService.Set(cacheKey, partReadDto, TimeSpan.FromHours(1));

        return partReadDto;
    }

    public ProductionPartReturnFilterDto GetFilter(ProductionPartGetFilterDto filter)
    {
        string cacheKey = $"ProductionPartFilter:{filter.ProductionOrderIdEqual}:{filter.PartCodeContains}:{filter.QuantityUsedEqual}:{filter.PageSize}:{filter.PageNumber}";

        var cachedResult = _cacheService.Get<ProductionPartReturnFilterDto>(cacheKey);

        if (cachedResult != null) return cachedResult;

        var parts = _getFilter.GetFilter(filter);

        _cacheService.Set(cacheKey, parts, TimeSpan.FromHours(1));

        return parts;
    }

    public ProductionPartReadDto Create(ProductionPartCreateDto productionPartCreateDto)
    {
        if (!ValidateCommonProductionPartFields(productionPartCreateDto)) return null;

        var part = _productionPartFactory.MapToProductionPart(productionPartCreateDto);

        part = _createCommand.Create(part);

        return _productionPartFactory.MapToProductionOrderReadDto(part);
    }

    public void Update(ProductionPartUpdateDto productionPartUpdateDto)
    {
        var part = ValidateProductionPartIds(productionPartUpdateDto.Id);
        if (part is null) return;

        if (!ValidateCommonProductionPartFields(productionPartUpdateDto)) return;

        _productionPartFactory.MapToProductionPartFromUpdateDto(part, productionPartUpdateDto);

        _updateCommand.Update(part);

        _cacheService.Remove($"ProductionPart:{productionPartUpdateDto.Id}");
        _cacheService.RemoveByPrefix("ProductionPartFilter:");
    }

    public void Delete(Guid id)
    {
        var part = ValidateProductionPartIds(id);

        if (part is null) return;

        _deleteCommand.Delete(part);

        _cacheService.Remove($"ProductionPart:{id}");
        _cacheService.RemoveByPrefix("ProductionPartFilter:");
    }

    private ProductionPart? ValidateProductionPartIds(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return null;
        }

        var part = _getByElement.GetById(id);

        if (part is null)
        {
            _notificationContext.AddNotification("Parte de produção não encontrada.");
            return null;
        }

        return part;
    }

    private bool ValidateCommonProductionPartFields(dynamic partDto)
    {
        bool isValid = true;

        if (partDto.ProductionOrderId == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID da Ordem de Produção' não pode ser vazio.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(partDto.PartCode))
        {
            _notificationContext.AddNotification("O campo 'Código da Peça' não pode estar vazio.");
            isValid = false;
        }

        if (partDto.QuantityUsed <= 0)
        {
            _notificationContext.AddNotification("A quantidade utilizada deve ser maior que zero.");
            isValid = false;
        }

        return isValid;
    }
}