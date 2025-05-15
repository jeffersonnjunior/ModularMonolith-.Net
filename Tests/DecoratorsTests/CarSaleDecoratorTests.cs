using System;
using Moq;
using Xunit;
using Common.Exceptions;
using Common.ICache.Services;
using Modules.Sales.Decorators;
using Modules.Sales.Dtos.CarSaleDtos;
using Modules.Sales.Entities;
using Modules.Sales.Enums;
using Modules.Sales.Interfaces.ICommands.ICarSaleCommands;
using Modules.Sales.Interfaces.IFactories;
using Modules.Sales.Interfaces.IQuerys.ICarSaleQuerys;
using Modules.Sales.Querys.CarSaleQuerys;
using Moq.Protected;

namespace Tests.DecoratorsTests;

public class CarSaleDecoratorTests
{
    private readonly Mock<ICarSaleGetByElement> _getByElement = new();
    private readonly Mock<ICarSaleGetFilter> _getFilter = new();
    private readonly Mock<ICarSaleCreateCommand> _createCommand = new();
    private readonly Mock<ICarSaleUpdateCommand> _updateCommand = new();
    private readonly Mock<ICarSaleDeleteCommand> _deleteCommand = new();
    private readonly Mock<ICarSaleFactory> _carSaleFactory = new();
    private readonly Mock<ICacheService> _cacheService = new();
    private readonly NotificationContext _notificationContext = new();

    private CarSaleDecorator CreateDecorator() =>
        new CarSaleDecorator(
            _getByElement.Object,
            _getFilter.Object,
            _createCommand.Object,
            _updateCommand.Object,
            _deleteCommand.Object,
            _carSaleFactory.Object,
            _cacheService.Object,
            _notificationContext
        );

    [Fact]
    public void GetById_ReturnsFromCache_IfExists()
    {
        var id = Guid.NewGuid();
        var cached = new CarSaleReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(new CarSale { Id = id });
        _cacheService.Setup(x => x.Get<CarSaleReadDto>($"CarSale:{id}")).Returns(cached);

        var decorator = CreateDecorator();

        var result = decorator.GetById(id);

        Assert.Equal(cached, result);
        _carSaleFactory.Verify(x => x.MapToCarSaleReadDto(It.IsAny<CarSale>()), Times.Never);
    }

    [Fact]
    public void GetById_ReturnsMappedAndSetsCache_IfNotCached()
    {
        var id = Guid.NewGuid();
        var sale = new CarSale { Id = id };
        var mapped = new CarSaleReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(sale);
        _cacheService.Setup(x => x.Get<CarSaleReadDto>($"CarSale:{id}")).Returns((CarSaleReadDto)null);
        _carSaleFactory.Setup(x => x.MapToCarSaleReadDto(sale)).Returns(mapped);

        var decorator = CreateDecorator();

        var result = decorator.GetById(id);

        Assert.Equal(mapped, result);
        _cacheService.Verify(x => x.Set($"CarSale:{id}", mapped, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void GetById_ReturnsNull_IfNotFound()
    {
        var id = Guid.NewGuid();
        _getByElement.Setup(x => x.GetById(id)).Returns((CarSale)null);

        var decorator = CreateDecorator();

        var result = decorator.GetById(id);

        Assert.Null(result);
    }

    [Fact]
    public void GetFilter_ReturnsFromCache_IfExists()
    {
        var filter = new CarSaleGetFilterDto { ProductionOrderIdEqual = Guid.NewGuid(), StatusEqual = SaleStatus.Pendente, PageNumber = 1, PageSize = 10 };
        var cacheKey = $"CarSaleFilter:{filter.ProductionOrderIdEqual}:{filter.StatusEqual}:{filter.SoldAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var cached = new CarSaleReturnFilterDto();
        _cacheService.Setup(x => x.Get<CarSaleReturnFilterDto>(cacheKey)).Returns(cached);

        var decorator = CreateDecorator();

        var result = decorator.GetFilter(filter);

        Assert.Equal(cached, result);
        _getFilter.Verify(x => x.GetFilter(It.IsAny<CarSaleGetFilterDto>()), Times.Never);
    }

    [Fact]
    public void GetFilter_ReturnsAndSetsCache_IfNotCached()
    {
        var filter = new CarSaleGetFilterDto { ProductionOrderIdEqual = Guid.NewGuid(), StatusEqual = SaleStatus.Pendente, PageNumber = 1, PageSize = 10 };
        var cacheKey = $"CarSaleFilter:{filter.ProductionOrderIdEqual}:{filter.StatusEqual}:{filter.SoldAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var resultDto = new CarSaleReturnFilterDto();
        _cacheService.Setup(x => x.Get<CarSaleReturnFilterDto>(cacheKey)).Returns((CarSaleReturnFilterDto)null);
        _getFilter.Setup(x => x.GetFilter(filter)).Returns(resultDto);

        var decorator = CreateDecorator();

        var result = decorator.GetFilter(filter);

        Assert.Equal(resultDto, result);
        _cacheService.Verify(x => x.Set(cacheKey, resultDto, It.IsAny<TimeSpan>()), Times.Once);
    }
    [Fact]
    public void Create_ReturnsMappedDto_WhenValid()
    {
        var id = Guid.NewGuid();
        var createDto = new CarSaleCreateDto
        {
            ProductionOrderId = Guid.NewGuid(),
            Status = SaleStatus.Pendente,
            SoldAt = DateTime.UtcNow
        };

        var carSale = new CarSale { Id = id };
        var readDto = new CarSaleReadDto { Id = id };

        _carSaleFactory.Setup(x => x.MapToCarSale(createDto)).Returns(carSale);
        _createCommand.Setup(x => x.Create(carSale)).Returns(carSale);
        _carSaleFactory.Setup(x => x.MapToCarSaleReadDto(carSale)).Returns(readDto);

        var decorator = CreateDecorator();

        var result = decorator.Create(createDto);

        Assert.Equal(readDto, result);
    }

    [Fact]
    public void Update_ValidInput_UpdatesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var updateDto = new CarSaleUpdateDto { Id = id, ProductionOrderId = Guid.NewGuid(), Status = SaleStatus.Pendente, SoldAt = DateTime.UtcNow };
        var sale = new CarSale { Id = id };

        _getByElement.Setup(x => x.GetById(id)).Returns(sale);
        _carSaleFactory.Setup(x => x.MapToCarSaleFromUpdateDto(sale, updateDto));
        _updateCommand.Setup(x => x.Update(sale));

        var decorator = CreateDecorator();

        decorator.Update(updateDto);

        _carSaleFactory.Verify(x => x.MapToCarSaleFromUpdateDto(sale, updateDto), Times.Once);
        _updateCommand.Verify(x => x.Update(sale), Times.Once);
        _cacheService.Verify(x => x.Remove($"CarSale:{id}"), Times.Once);
        _cacheService.Verify(x => x.RemoveByPrefix("CarSaleFilter:"), Times.Once);
    }

    [Fact]
    public void Delete_ValidInput_DeletesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var sale = new CarSale { Id = id };

        _getByElement.Setup(x => x.GetById(id)).Returns(sale);

        var decorator = CreateDecorator();

        decorator.Delete(id);

        _deleteCommand.Verify(x => x.Delete(sale), Times.Once);
        _cacheService.Verify(x => x.Remove($"CarSale:{id}"), Times.Once);
        _cacheService.Verify(x => x.RemoveByPrefix("CarSaleFilter:"), Times.Once);
    }
}
