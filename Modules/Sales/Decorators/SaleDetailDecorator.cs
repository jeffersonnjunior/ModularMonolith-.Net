using Common.Exceptions;
using Common.ICache.Services;
using Modules.Sales.Dtos.SaleDetailDtos;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;
using Modules.Sales.Interfaces.IDecorators;
using Modules.Sales.Interfaces.IFactories;
using Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;
using Modules.Sales.Querys.SaleDetailQuerys;

namespace Modules.Sales.Decorators;

public class SaleDetailDecorator : ISaleDetailDecorator
{
    private readonly ISaleDetailGetByElement _getByElement;
    private readonly ISaleDetailGetFilter _getFilter;
    private readonly ISaleDetailCreateCommand _createCommand;
    private readonly ISaleDetailUpdateCommand _updateCommand;
    private readonly ISaleDetailDeleteCommand _deleteCommand;
    private readonly ISaleDetailFactory _saleDetailFactory;
    private readonly ICacheService _cacheService;
    private readonly NotificationContext _notificationContext;

    public SaleDetailDecorator(
        ISaleDetailGetByElement getByElement,
        ISaleDetailGetFilter getFilter,
        ISaleDetailCreateCommand createCommand,
        ISaleDetailUpdateCommand updateCommand,
        ISaleDetailDeleteCommand deleteCommand,
        ISaleDetailFactory saleDetailFactory,
        ICacheService cacheService,
        NotificationContext notificationContext)
    {
        _getByElement = getByElement;
        _getFilter = getFilter;
        _createCommand = createCommand;
        _updateCommand = updateCommand;
        _deleteCommand = deleteCommand;
        _saleDetailFactory = saleDetailFactory;
        _cacheService = cacheService;
        _notificationContext = notificationContext;
    }

    public SaleDetailReadDto GetById(Guid id)
    {
        var detail = ValidateSaleDetailIds(id);

        if (detail is null) return null;

        string cacheKey = $"SaleDetail:{id}";

        var cachedDetail = _cacheService.Get<SaleDetailReadDto>(cacheKey);
        if (cachedDetail != null) return cachedDetail;

        SaleDetailReadDto detailReadDto = _saleDetailFactory.MapToSaleDetailReadDto(detail);

        _cacheService.Set(cacheKey, detailReadDto, TimeSpan.FromHours(1));

        return detailReadDto;
    }

    public SaleDetailReturnFilterDto GetFilter(SaleDetailGetFilterDto filter)
    {
        string cacheKey = $"SaleDetailFilter:{filter.CarSaleIdEqual}:{filter.BuyerNameContains}:{filter.PriceContainsEqual}:{filter.PageSize}:{filter.PageNumber}";

        var cachedResult = _cacheService.Get<SaleDetailReturnFilterDto>(cacheKey);

        if (cachedResult != null) return cachedResult;

        var details = _getFilter.GetFilter(filter);

        _cacheService.Set(cacheKey, details, TimeSpan.FromHours(1));

        return details;
    }

    public SaleDetailReadDto Create(SaleDetailCreateDto saleDetailCreateDto)
    {
        if (!ValidateCommonSaleDetailFields(saleDetailCreateDto)) return null;

        var detail = _saleDetailFactory.MapToSaleDetail(saleDetailCreateDto);

        detail = _createCommand.Create(detail);

        _cacheService.Remove($"CarSale:{saleDetailCreateDto.CarSaleId}");
        _cacheService.RemoveByPrefix("SaleDetailFilter:");

        return _saleDetailFactory.MapToSaleDetailReadDto(detail);
    }

    public void Update(SaleDetailUpdateDto saleDetailUpdateDto)
    {
        var detail = ValidateSaleDetailIds(saleDetailUpdateDto.Id);
        if (detail is null) return;

        if (!ValidateCommonSaleDetailFields(saleDetailUpdateDto)) return;

        _saleDetailFactory.MapToSaleDetailFromUpdateDto(detail, saleDetailUpdateDto);

        _updateCommand.Update(detail);

        _cacheService.Remove($"SaleDetail:{saleDetailUpdateDto.Id}");
        _cacheService.Remove($"CarSale:{saleDetailUpdateDto.CarSaleId}");
        _cacheService.RemoveByPrefix("SaleDetailFilter:");
    }

    public void Delete(Guid id)
    {
        var detail = ValidateSaleDetailIds(id);

        if (detail is null) return;

        var carSaleId = detail.CarSaleId;

        _deleteCommand.Delete(detail);

        _cacheService.Remove($"SaleDetail:{id}");
        _cacheService.Remove($"CarSale:{carSaleId}");
        _cacheService.RemoveByPrefix("SaleDetailFilter:");
    }

    private SaleDetail? ValidateSaleDetailIds(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'ID' não pode ser vazio.");
            return null;
        }

        var detail = _getByElement.GetById(id);

        if (detail is null)
        {
            _notificationContext.AddNotification("Detalhe de venda não encontrado.");
            return null;
        }

        return detail;
    }

    private bool ValidateCommonSaleDetailFields(dynamic detailDto)
    {
        bool isValid = true;

        if (detailDto.CarSaleId == Guid.Empty)
        {
            _notificationContext.AddNotification("O campo 'CarSaleId' não pode ser vazio.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(detailDto.BuyerName))
        {
            _notificationContext.AddNotification("O campo 'BuyerName' não pode ser vazio.");
            isValid = false;
        }

        if (detailDto.Price <= 0)
        {
            _notificationContext.AddNotification("O preço deve ser maior que zero.");
            isValid = false;
        }

        if (detailDto.Discount < 0)
        {
            _notificationContext.AddNotification("O desconto não pode ser negativo.");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(detailDto.PaymentMethod))
        {
            _notificationContext.AddNotification("O método de pagamento não pode ser vazio.");
            isValid = false;
        }

        return isValid;
    }
}