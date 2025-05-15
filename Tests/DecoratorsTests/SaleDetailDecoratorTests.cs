using Common.Exceptions;
using Common.ICache.Services;
using Modules.Sales.Decorators;
using Modules.Sales.Dtos.SaleDetailDtos;
using Modules.Sales.Entities;
using Modules.Sales.Interfaces.ICommands.ISaleDetailCommands;
using Modules.Sales.Interfaces.IFactories;
using Modules.Sales.Interfaces.IQuerys.ISaleDetailQuerys;
using Moq;

namespace Tests.DecoratorsTests;

public class SaleDetailDecoratorTests
{
    private readonly Mock<ISaleDetailGetByElement> _getByElement = new();
    private readonly Mock<ISaleDetailGetFilter> _getFilter = new();
    private readonly Mock<ISaleDetailCreateCommand> _createCommand = new();
    private readonly Mock<ISaleDetailUpdateCommand> _updateCommand = new();
    private readonly Mock<ISaleDetailDeleteCommand> _deleteCommand = new();
    private readonly Mock<ISaleDetailFactory> _factory = new();
    private readonly Mock<ICacheService> _cache = new();
    private readonly NotificationContext _notificationContext = new();

    private SaleDetailDecorator CreateDecorator() =>
        new(
            _getByElement.Object,
            _getFilter.Object,
            _createCommand.Object,
            _updateCommand.Object,
            _deleteCommand.Object,
            _factory.Object,
            _cache.Object,
            _notificationContext
        );

    [Fact]
    public void GetById_ReturnsNull_WhenIdIsEmpty()
    {
        var decorator = CreateDecorator();
        var result = decorator.GetById(Guid.Empty);
        Assert.Null(result);
        Assert.Contains(_notificationContext.GetNotifications(), n => n.Message.Contains("ID"));
    }

    [Fact]
    public void GetById_ReturnsNull_WhenNotFound()
    {
        _getByElement.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((SaleDetail)null);
        var decorator = CreateDecorator();
        var result = decorator.GetById(Guid.NewGuid());
        Assert.Null(result);
        Assert.Contains(_notificationContext.GetNotifications(), n => n.Message.Contains("não encontrado"));
    }

    [Fact]
    public void GetById_ReturnsFromCache_IfExists()
    {
        var id = Guid.NewGuid();
        var cached = new SaleDetailReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(new SaleDetail { Id = id });
        _cache.Setup(x => x.Get<SaleDetailReadDto>($"SaleDetail:{id}")).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetById_MapsAndCaches_WhenNotInCache()
    {
        var id = Guid.NewGuid();
        var entity = new SaleDetail { Id = id };
        var dto = new SaleDetailReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);
        _cache.Setup(x => x.Get<SaleDetailReadDto>($"SaleDetail:{id}")).Returns((SaleDetailReadDto)null);
        _factory.Setup(x => x.MapToSaleDetailReadDto(entity)).Returns(dto);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(dto, result);
        _cache.Verify(x => x.Set($"SaleDetail:{id}", dto, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void GetFilter_QueriesAndCaches_WhenNotInCache()
    {
        var filter = new SaleDetailGetFilterDto();
        var cacheKey = $"SaleDetailFilter:{filter.CarSaleIdEqual}:{filter.BuyerNameContains}:{filter.PriceContainsEqual}:{filter.PageSize}:{filter.PageNumber}";
        var details = new SaleDetailReturnFilterDto();
        _cache.Setup(x => x.Get<SaleDetailReturnFilterDto>(cacheKey)).Returns((SaleDetailReturnFilterDto)null);
        _getFilter.Setup(x => x.GetFilter(filter)).Returns(details);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(details, result);
        _cache.Verify(x => x.Set(cacheKey, details, It.IsAny<TimeSpan>()), Times.Once);
    }


    [Fact]
    public void Create_ReturnsNull_IfValidationFails()
    {
        var dto = new SaleDetailCreateDto { CarSaleId = Guid.Empty };
        var decorator = CreateDecorator();
        var result = decorator.Create(dto);
        Assert.Null(result);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Create_MapsAndCreatesAndRemovesCache()
    {
        var dto = new SaleDetailCreateDto
        {
            CarSaleId = Guid.NewGuid(),
            BuyerName = "Test",
            Price = 100,
            Discount = 0,
            PaymentMethod = "Cash"
        };
        var entity = new SaleDetail { Id = Guid.NewGuid(), CarSaleId = dto.CarSaleId };
        var created = new SaleDetail { Id = Guid.NewGuid(), CarSaleId = dto.CarSaleId };
        var readDto = new SaleDetailReadDto { Id = created.Id, CarSaleId = dto.CarSaleId };

        _factory.Setup(x => x.MapToSaleDetail(dto)).Returns(entity);
        _createCommand.Setup(x => x.Create(entity)).Returns(created);
        _factory.Setup(x => x.MapToSaleDetailReadDto(created)).Returns(readDto);

        var decorator = CreateDecorator();
        var result = decorator.Create(dto);

        Assert.Equal(readDto, result);
        _cache.Verify(x => x.Remove($"CarSale:{dto.CarSaleId}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("SaleDetailFilter:"), Times.Once);
    }

    [Fact]
    public void Update_DoesNothing_IfIdInvalid()
    {
        var dto = new SaleDetailUpdateDto { Id = Guid.Empty };
        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_DoesNothing_IfValidationFails()
    {
        var id = Guid.NewGuid();
        var entity = new SaleDetail { Id = id };
        var dto = new SaleDetailUpdateDto { Id = id, CarSaleId = Guid.Empty };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_MapsAndUpdatesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var carSaleId = Guid.NewGuid();
        var entity = new SaleDetail { Id = id, CarSaleId = carSaleId };
        var dto = new SaleDetailUpdateDto
        {
            Id = id,
            CarSaleId = carSaleId,
            BuyerName = "Test",
            Price = 100,
            Discount = 0,
            PaymentMethod = "Cash"
        };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);

        _factory.Verify(x => x.MapToSaleDetailFromUpdateDto(entity, dto), Times.Once);
        _updateCommand.Verify(x => x.Update(entity), Times.Once);
        _cache.Verify(x => x.Remove($"SaleDetail:{id}"), Times.Once);
        _cache.Verify(x => x.Remove($"CarSale:{carSaleId}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("SaleDetailFilter:"), Times.Once);
    }

    [Fact]
    public void Delete_DoesNothing_IfIdInvalid()
    {
        var decorator = CreateDecorator();
        decorator.Delete(Guid.Empty);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Delete_DeletesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var carSaleId = Guid.NewGuid();
        var entity = new SaleDetail { Id = id, CarSaleId = carSaleId };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Delete(id);

        _deleteCommand.Verify(x => x.Delete(entity), Times.Once);
        _cache.Verify(x => x.Remove($"SaleDetail:{id}"), Times.Once);
        _cache.Verify(x => x.Remove($"CarSale:{carSaleId}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("SaleDetailFilter:"), Times.Once);
    }
}
