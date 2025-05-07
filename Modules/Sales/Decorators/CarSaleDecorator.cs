using Common.Exceptions;
using Common.ICache.Services;
using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Entities;
using Modules.Sales.Enums;
using Modules.Sales.Interfaces.ICommands.ICarSaleCommands;
using Modules.Sales.Interfaces.IDecorators;
using Modules.Sales.Interfaces.IFactories;
using Modules.Sales.Interfaces.IQuerys.ICarSaleQuerys;
using Modules.Sales.Querys.CarSaleQuerys;

namespace Modules.Sales.Decorators;

public class CarSaleDecorator : ICarSaleDecorator
{
    private readonly ICarSaleGetByElement _getByElement;
    private readonly ICarSaleGetFilter _getFilter;
    private readonly ICarSaleCreateCommand _createCommand;
    private readonly ICarSaleUpdateCommand _updateCommand;
    private readonly ICarSaleDeleteCommand _deleteCommand;
    private readonly ICarSaleFactory _carSaleFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;

    public CarSaleDecorator(
        ICarSaleGetByElement getByElement,
        ICarSaleGetFilter getFilter,
        ICarSaleCreateCommand createCommand,
        ICarSaleUpdateCommand updateCommand,
        ICarSaleDeleteCommand deleteCommand,
        ICarSaleFactory carSaleFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _getByElement = getByElement;
        _getFilter = getFilter;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _carSaleFactory = carSaleFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
    }

    public CarSaleReadDto GetById(Guid id)
    {
        var sale = ValidateCarSaleIds(id);

        if (sale is null) return null;

        string cacheKey = $"CarSale:{id}";

        var cachedSale = _cacheService.Get<CarSaleReadDto>(cacheKey);
        if (cachedSale != null) return cachedSale;

        CarSaleReadDto saleReadDto = _carSaleFactory.MapToCarSaleReadDto(sale);

        _cacheService.Set(cacheKey, saleReadDto, TimeSpan.FromHours(1));

        return saleReadDto;
    }

    public CarSaleReturnFilterDto GetFilter(CarSaleGetFilterDto filter)
    {
        string cacheKey = $"CarSaleFilter:{filter.ProductionOrderIdEqual}:{filter.StatusEqual}:{filter.SoldAtEqual}:{filter.PageSize}:{filter.PageNumber}";

        var cachedResult = _cacheService.Get<CarSaleReturnFilterDto>(cacheKey);

        if (cachedResult != null) return cachedResult;

        var sales = _getFilter.GetFilter(filter);

        _cacheService.Set(cacheKey, sales, TimeSpan.FromHours(1));

        return sales;
    }

    public CarSaleReadDto Create(CarSaleCreateDto carSaleCreateDto)
    {
        if (!ValidateCommonCarSaleFields(carSaleCreateDto)) return null;

        var sale = _carSaleFactory.MapToCarSale(carSaleCreateDto);

        sale = _createCommand.Create(sale);

        return _carSaleFactory.MapToCarSaleReadDto(sale);
    }

    public void Update(CarSaleUpdateDto carSaleUpdateDto)
    {
        var sale = ValidateCarSaleIds(carSaleUpdateDto.Id);
        if (sale is null) return;

        if (!ValidateCommonCarSaleFields(carSaleUpdateDto)) return;

        _carSaleFactory.MapToCarSaleFromUpdateDto(sale, carSaleUpdateDto);

        _updateCommand.Update(sale);

        _cacheService.Remove($"CarSale:{carSaleUpdateDto.Id}");
        _cacheService.RemoveByPrefix("CarSaleFilter:");
    }

    public void Delete(Guid id)
    {
        var sale = ValidateCarSaleIds(id);

        if (sale is null) return;

        _deleteCommand.Delete(sale);

        _cacheService.Remove($"CarSale:{id}");
        _cacheService.RemoveByPrefix("CarSaleFilter:");
    }

    private CarSale? ValidateCarSaleIds(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return null;
        }

        var sale = _getByElement.GetById(id);

        if (sale is null)
        {
            _notificationContext.AddNotification("Venda de carro não encontrada.");
            return null;
        }

        return sale;
    }

    private bool ValidateCommonCarSaleFields(dynamic saleDto)
    {
        bool isValid = true;

        if (saleDto.ProductionOrderId == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ProductionOrderId' não pode ser vazio.");
            isValid = false;
        }

        if (!Enum.IsDefined(typeof(SaleStatus), saleDto.Status))
        {
            _notificationContext.AddNotification("Status de venda inválido.");
            isValid = false;
        }

        if (saleDto.SoldAt.HasValue && saleDto.SoldAt > DateTime.UtcNow)
        {
            _notificationContext.AddNotification("A data de venda não pode ser no futuro.");
            isValid = false;
        }

        return isValid;
    }
}