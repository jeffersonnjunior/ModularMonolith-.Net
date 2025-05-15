using Common.Exceptions;
using Common.ICache.Services;
using Modules.Production.Decorators;
using Modules.Production.Dtos.ProductionPartDtos;
using Modules.Production.Entities;
using Modules.Production.Interfaces.ICommands.IProductionPartCommands;
using Modules.Production.Interfaces.IFactories;
using Modules.Production.Interfaces.IQuerys.IProductionPartQuerys;
using Moq;

namespace Tests.DecoratorsTests;

public class ProductionPartDecoratorTests
{
    private readonly Mock<IProductionPartGetByElement> _getByElement = new();
    private readonly Mock<IProductionPartGetFilter> _getFilter = new();
    private readonly Mock<IProductionPartCreateCommand> _createCommand = new();
    private readonly Mock<IProductionPartUpdateCommand> _updateCommand = new();
    private readonly Mock<IProductionPartDeleteCommand> _deleteCommand = new();
    private readonly Mock<IProductionPartFactory> _factory = new();
    private readonly Mock<ICacheService> _cache = new();
    private readonly NotificationContext _notificationContext = new();

    private ProductionPartDecorator CreateDecorator() =>
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
        _getByElement.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((ProductionPart)null);
        var decorator = CreateDecorator();
        var result = decorator.GetById(Guid.NewGuid());
        Assert.Null(result);
        Assert.Contains(_notificationContext.GetNotifications(), n => n.Message.Contains("não encontrada"));
    }

    [Fact]
    public void GetById_ReturnsFromCache_IfExists()
    {
        var id = Guid.NewGuid();
        var cached = new ProductionPartReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(new ProductionPart { Id = id });
        _cache.Setup(x => x.Get<ProductionPartReadDto>($"ProductionPart:{id}")).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetById_MapsAndCaches_WhenNotInCache()
    {
        var id = Guid.NewGuid();
        var entity = new ProductionPart { Id = id };
        var dto = new ProductionPartReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);
        _cache.Setup(x => x.Get<ProductionPartReadDto>($"ProductionPart:{id}")).Returns((ProductionPartReadDto)null);
        _factory.Setup(x => x.MapToProductionOrderReadDto(entity)).Returns(dto);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(dto, result);
        _cache.Verify(x => x.Set($"ProductionPart:{id}", dto, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void GetFilter_ReturnsFromCache_IfExists()
    {
        var filter = new ProductionPartGetFilterDto();
        var cacheKey = $"ProductionPartFilter:{filter.ProductionOrderIdEqual}:{filter.PartCodeContains}:{filter.QuantityUsedEqual}:{filter.PageSize}:{filter.PageNumber}";
        var cached = new ProductionPartReturnFilterDto();
        _cache.Setup(x => x.Get<ProductionPartReturnFilterDto>(cacheKey)).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetFilter_QueriesAndCaches_WhenNotInCache()
    {
        var filter = new ProductionPartGetFilterDto();
        var cacheKey = $"ProductionPartFilter:{filter.ProductionOrderIdEqual}:{filter.PartCodeContains}:{filter.QuantityUsedEqual}:{filter.PageSize}:{filter.PageNumber}";
        var details = new ProductionPartReturnFilterDto();
        _cache.Setup(x => x.Get<ProductionPartReturnFilterDto>(cacheKey)).Returns((ProductionPartReturnFilterDto)null);
        _getFilter.Setup(x => x.GetFilter(filter)).Returns(details);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(details, result);
        _cache.Verify(x => x.Set(cacheKey, details, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void Create_ReturnsNull_IfValidationFails()
    {
        var dto = new ProductionPartCreateDto { ProductionOrderId = Guid.Empty };
        var decorator = CreateDecorator();
        var result = decorator.Create(dto);
        Assert.Null(result);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Create_MapsAndCreates()
    {
        var dto = new ProductionPartCreateDto
        {
            ProductionOrderId = Guid.NewGuid(),
            PartCode = "P123",
            QuantityUsed = 5
        };
        var entity = new ProductionPart { Id = Guid.NewGuid(), ProductionOrderId = dto.ProductionOrderId };
        var created = new ProductionPart { Id = Guid.NewGuid(), ProductionOrderId = dto.ProductionOrderId };
        var readDto = new ProductionPartReadDto { Id = created.Id, ProductionOrderId = dto.ProductionOrderId };

        _factory.Setup(x => x.MapToProductionPart(dto)).Returns(entity);
        _createCommand.Setup(x => x.Create(entity)).Returns(created);
        _factory.Setup(x => x.MapToProductionOrderReadDto(created)).Returns(readDto);

        var decorator = CreateDecorator();
        var result = decorator.Create(dto);

        Assert.Equal(readDto, result);
    }

    [Fact]
    public void Update_DoesNothing_IfIdInvalid()
    {
        var dto = new ProductionPartUpdateDto { Id = Guid.Empty };
        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_DoesNothing_IfValidationFails()
    {
        var id = Guid.NewGuid();
        var entity = new ProductionPart { Id = id };
        var dto = new ProductionPartUpdateDto { Id = id, ProductionOrderId = Guid.Empty };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_MapsAndUpdatesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var entity = new ProductionPart { Id = id, ProductionOrderId = Guid.NewGuid() };
        var dto = new ProductionPartUpdateDto
        {
            Id = id,
            ProductionOrderId = entity.ProductionOrderId,
            PartCode = "P123",
            QuantityUsed = 5
        };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);

        _factory.Verify(x => x.MapToProductionPartFromUpdateDto(entity, dto), Times.Once);
        _updateCommand.Verify(x => x.Update(entity), Times.Once);
        _cache.Verify(x => x.Remove($"ProductionPart:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("ProductionPartFilter:"), Times.Once);
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
        var entity = new ProductionPart { Id = id, ProductionOrderId = Guid.NewGuid() };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Delete(id);

        _deleteCommand.Verify(x => x.Delete(entity), Times.Once);
        _cache.Verify(x => x.Remove($"ProductionPart:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("ProductionPartFilter:"), Times.Once);
    }
}
