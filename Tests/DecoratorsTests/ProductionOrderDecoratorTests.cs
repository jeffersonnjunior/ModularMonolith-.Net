using System;
using Common.Exceptions;
using Common.ICache.Services;
using Modules.Production.Decorators;
using Modules.Production.Dtos.ProductionOrderDtos;
using Modules.Production.Entities;
using Modules.Production.Enums;
using Modules.Production.Interfaces.ICommands.IProductionOrderCommands;
using Modules.Production.Interfaces.IFactories;
using Modules.Production.Interfaces.IQuerys.IProductionOrderQuerys;
using Moq;
using Xunit;

namespace Tests.DecoratorsTests;

public class ProductionOrderDecoratorTests
{
    private readonly Mock<IProductionOrderGetByElement> _getByElement = new();
    private readonly Mock<IProductionOrderGetFilter> _getFilter = new();
    private readonly Mock<IProductionOrderCreateCommand> _createCommand = new();
    private readonly Mock<IProductionOrderUpdateCommand> _updateCommand = new();
    private readonly Mock<IProductionOrderDeleteCommand> _deleteCommand = new();
    private readonly Mock<IProductionOrderFactory> _factory = new();
    private readonly Mock<ICacheService> _cache = new();
    private readonly NotificationContext _notificationContext = new();

    private ProductionOrderDecorator CreateDecorator() =>
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
        _getByElement.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((ProductionOrder)null);
        var decorator = CreateDecorator();
        var result = decorator.GetById(Guid.NewGuid());
        Assert.Null(result);
        Assert.Contains(_notificationContext.GetNotifications(), n => n.Message.Contains("não encontrada"));
    }

    [Fact]
    public void GetById_ReturnsFromCache_IfExists()
    {
        var id = Guid.NewGuid();
        var cached = new ProductionOrderReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(new ProductionOrder { Id = id });
        _cache.Setup(x => x.Get<ProductionOrderReadDto>($"ProductionOrder:{id}")).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetById_MapsAndCaches_WhenNotInCache()
    {
        var id = Guid.NewGuid();
        var entity = new ProductionOrder { Id = id };
        var dto = new ProductionOrderReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);
        _cache.Setup(x => x.Get<ProductionOrderReadDto>($"ProductionOrder:{id}")).Returns((ProductionOrderReadDto)null);
        _factory.Setup(x => x.MapToProductionOrderReadDto(entity)).Returns(dto);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(dto, result);
        _cache.Verify(x => x.Set($"ProductionOrder:{id}", dto, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void GetFilter_ReturnsFromCache_IfExists()
    {
        var filter = new ProductionOrderGetFilterDto();
        var cacheKey = $"ProductionOrderFilter:{filter.ModelContains}:{filter.ProductionStatusEqual}:{filter.CreatedAtEqual}:{filter.CompletedAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var cached = new ProductionOrderReturnFilterDto();
        _cache.Setup(x => x.Get<ProductionOrderReturnFilterDto>(cacheKey)).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetFilter_QueriesAndCaches_WhenNotInCache()
    {
        var filter = new ProductionOrderGetFilterDto();
        var cacheKey = $"ProductionOrderFilter:{filter.ModelContains}:{filter.ProductionStatusEqual}:{filter.CreatedAtEqual}:{filter.CompletedAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var details = new ProductionOrderReturnFilterDto();
        _cache.Setup(x => x.Get<ProductionOrderReturnFilterDto>(cacheKey)).Returns((ProductionOrderReturnFilterDto)null);
        _getFilter.Setup(x => x.GetFilter(filter)).Returns(details);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(details, result);
        _cache.Verify(x => x.Set(cacheKey, details, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void Create_ReturnsNull_IfValidationFails()
    {
        var dto = new ProductionOrderCreateDto { Model = null };
        var decorator = CreateDecorator();
        var result = decorator.Create(dto);
        Assert.Null(result);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Create_MapsAndCreates()
    {
        var dto = new ProductionOrderCreateDto
        {
            Model = "ModelX",
            ProductionStatus = ProductionStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        var entity = new ProductionOrder { Id = Guid.NewGuid(), Model = dto.Model };
        var created = new ProductionOrder { Id = Guid.NewGuid(), Model = dto.Model };
        var readDto = new ProductionOrderReadDto { Id = created.Id, Model = dto.Model };

        _factory.Setup(x => x.MapToProductionOrder(dto)).Returns(entity);
        _createCommand.Setup(x => x.Create(entity)).Returns(created);
        _factory.Setup(x => x.MapToProductionOrderReadDto(created)).Returns(readDto);

        var decorator = CreateDecorator();
        var result = decorator.Create(dto);

        Assert.Equal(readDto, result);
    }

    [Fact]
    public void Update_DoesNothing_IfIdInvalid()
    {
        var dto = new ProductionOrderUpdateDto { Id = Guid.Empty };
        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_DoesNothing_IfValidationFails()
    {
        var id = Guid.NewGuid();
        var entity = new ProductionOrder { Id = id };
        var dto = new ProductionOrderUpdateDto { Id = id, Model = null };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_MapsAndUpdatesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var entity = new ProductionOrder { Id = id, Model = "ModelX" };
        var dto = new ProductionOrderUpdateDto
        {
            Id = id,
            Model = "ModelX",
            ProductionStatus = ProductionStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);

        _factory.Verify(x => x.MapToProductionOrderFromUpdateDto(entity, dto), Times.Once);
        _updateCommand.Verify(x => x.Update(entity), Times.Once);
        _cache.Verify(x => x.Remove($"ProductionOrder:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("ProductionOrderFilter:"), Times.Once);
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
        var entity = new ProductionOrder { Id = id, Model = "ModelX" };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Delete(id);

        _deleteCommand.Verify(x => x.Delete(entity), Times.Once);
        _cache.Verify(x => x.Remove($"ProductionOrder:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("ProductionOrderFilter:"), Times.Once);
    }
}
