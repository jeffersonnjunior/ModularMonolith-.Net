using Common.Exceptions;
using Common.ICache.Services;
using Modules.Production.Dtos.ProductionOrderDtos;
using Modules.Production.Entities;
using Modules.Production.Enums;
using Modules.Production.Interfaces.ICommands.IProductionOrderCommands;
using Modules.Production.Interfaces.IDecorators;
using Modules.Production.Interfaces.IFactories;
using Modules.Production.Interfaces.IQuerys.IProductionOrderQuerys;

namespace Modules.Production.Decorators;

public class ProductionOrderDecorator : IProductionOrderDecorator
{
    private readonly IProductionOrderGetByElement _getByElement;
    private readonly IProductionOrderGetFilter _getFilter;
    private readonly IProductionOrderCreateCommand _createCommand;
    private readonly IProductionOrderUpdateCommand _updateCommand;
    private readonly IProductionOrderDeleteCommand _deleteCommand;
    private readonly IProductionOrderFactory _productionOrderFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;

    public ProductionOrderDecorator(
        IProductionOrderGetByElement getByElement,
        IProductionOrderGetFilter getFilter,
        IProductionOrderCreateCommand createCommand,
        IProductionOrderUpdateCommand updateCommand,
        IProductionOrderDeleteCommand deleteCommand,
        IProductionOrderFactory productionOrderFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _getByElement = getByElement;
        _getFilter = getFilter;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _productionOrderFactory = productionOrderFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
    }

    public ProductionOrderReadDto GetById(Guid id)
    {
        var order = ValidateProductionOrderIds(id);

        if (order is null) return null;

        string cacheKey = $"ProductionOrder:{id}";

        var cachedOrder = _cacheService.Get<ProductionOrderReadDto>(cacheKey);
        if (cachedOrder != null) return cachedOrder;

        ProductionOrderReadDto orderReadDto = _productionOrderFactory.MapToProductionOrderReadDto(order);

        _cacheService.Set(cacheKey, orderReadDto, TimeSpan.FromHours(1));

        return orderReadDto;
    }

    public ProductionOrderReturnFilterDto GetFilter(ProductionOrderGetFilterDto filter)
    {
        string cacheKey = $"ProductionOrderFilter:{filter.ModelContains}:{filter.ProductionStatusEqual}:{filter.CreatedAtEqual}:{filter.CompletedAtEqual}:{filter.PageSize}:{filter.PageNumber}";

        var cachedResult = _cacheService.Get<ProductionOrderReturnFilterDto>(cacheKey);

        if (cachedResult != null) return cachedResult;

        var orders = _getFilter.GetFilter(filter);

        _cacheService.Set(cacheKey, orders, TimeSpan.FromHours(1));

        return orders;
    }

    public ProductionOrderReadDto Create(ProductionOrderCreateDto productionOrderCreateDto)
    {
        if (!ValidateCommonProductionOrderFields(productionOrderCreateDto)) return null;

        var order = _productionOrderFactory.MapToProductionOrder(productionOrderCreateDto);

        order = _createCommand.Create(order);

        return _productionOrderFactory.MapToProductionOrderReadDto(order);
    }

    public void Update(ProductionOrderUpdateDto productionOrderUpdateDto)
    {
        var order = ValidateProductionOrderIds(productionOrderUpdateDto.Id);
        if (order is null) return;

        if (!ValidateCommonProductionOrderFields(productionOrderUpdateDto)) return;

        _productionOrderFactory.MapToProductionOrderFromUpdateDto(order, productionOrderUpdateDto);

        _updateCommand.Update(order);

        _cacheService.Remove($"ProductionOrder:{productionOrderUpdateDto.Id}");
        _cacheService.RemoveByPrefix("ProductionOrderFilter:");
    }

    public void Delete(Guid id)
    {
        var order = ValidateProductionOrderIds(id);

        if (order is null) return;

        _deleteCommand.Delete(order);

        _cacheService.Remove($"ProductionOrder:{id}");
        _cacheService.RemoveByPrefix("ProductionOrderFilter:");
    }

    private ProductionOrder? ValidateProductionOrderIds(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return null;
        }

        var order = _getByElement.GetById(id);

        if (order is null)
        {
            _notificationContext.AddNotification("Ordem de produção não encontrada.");
            return null;
        }

        return order;
    }

    private bool ValidateCommonProductionOrderFields(dynamic orderDto)
    {
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(orderDto.Model))
        {
            _notificationContext.AddNotification("O campo 'Modelo' não pode estar vazio.");
            isValid = false;
        }

        if (!Enum.IsDefined(typeof(ProductionStatus), orderDto.ProductionStatus))
        {
            _notificationContext.AddNotification("Status de produção inválido.");
            isValid = false;
        }

        if (orderDto.CreatedAt == default(DateTime))
        {
            _notificationContext.AddNotification("O campo 'Data de Criação' deve ser uma data válida.");
            isValid = false;
        }

        if (orderDto.CompletedAt.HasValue && orderDto.CompletedAt < orderDto.CreatedAt)
        {
            _notificationContext.AddNotification("A data de conclusão não pode ser anterior à data de criação.");
            isValid = false;
        }

        return isValid;
    }
}