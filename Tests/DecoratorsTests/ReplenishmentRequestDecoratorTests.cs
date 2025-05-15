using System;
using Common.Exceptions;
using Common.ICache.Services;
using Modules.Inventory.Decorators;
using Modules.Inventory.Dtos.ReplenishmentRequestDtos;
using Modules.Inventory.Entities;
using Modules.Inventory.Enums;
using Modules.Inventory.Interfaces.ICommands.IReplenishmentRequestCommands;
using Modules.Inventory.Interfaces.IFactories;
using Modules.Inventory.Interfaces.IQuerys.IReplenishmentRequestQuerys;
using Moq;
using Xunit;

namespace Tests.DecoratorsTests;

public class ReplenishmentRequestDecoratorTests
{
    private readonly Mock<IReplenishmentRequestGetByElement> _getByElement = new();
    private readonly Mock<IReplenishmentRequestGetFilter> _getFilter = new();
    private readonly Mock<IReplenishmentRequestCreateCommand> _createCommand = new();
    private readonly Mock<IReplenishmentRequestUpdateCommand> _updateCommand = new();
    private readonly Mock<IReplenishmentRequestDeleteCommand> _deleteCommand = new();
    private readonly Mock<IReplenishmentRequestFactory> _factory = new();
    private readonly Mock<ICacheService> _cache = new();
    private readonly NotificationContext _notificationContext = new();

    private ReplenishmentRequestDecorator CreateDecorator() =>
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
        _getByElement.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((ReplenishmentRequest)null);
        var decorator = CreateDecorator();
        var result = decorator.GetById(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public void GetById_ReturnsFromCache_IfExists()
    {
        var id = Guid.NewGuid();
        var cached = new ReplenishmentRequestReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(new ReplenishmentRequest { Id = id });
        _cache.Setup(x => x.Get<ReplenishmentRequestReadDto>($"ReplenishmentRequest:{id}")).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetById_MapsAndCaches_WhenNotInCache()
    {
        var id = Guid.NewGuid();
        var entity = new ReplenishmentRequest { Id = id };
        var dto = new ReplenishmentRequestReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);
        _cache.Setup(x => x.Get<ReplenishmentRequestReadDto>($"ReplenishmentRequest:{id}")).Returns((ReplenishmentRequestReadDto)null);
        _factory.Setup(x => x.MapToReplenishmentRequestReadDto(entity)).Returns(dto);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(dto, result);
        _cache.Verify(x => x.Set($"ReplenishmentRequest:{id}", entity, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void GetFilter_ReturnsFromCache_IfExists()
    {
        var filter = new ReplenishmentRequestGetFilterDto();
        var cacheKey = $"ReplenishmentRequestFilter:{filter.PartCodeContains}:{filter.RequestedQuantityEqual}:{filter.ReplenishmentStatusEqual}:{filter.RequestedAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var cached = new ReplenishmentRequestReturnFilterDto();
        _cache.Setup(x => x.Get<ReplenishmentRequestReturnFilterDto>(cacheKey)).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetFilter_QueriesAndCaches_WhenNotInCache()
    {
        var filter = new ReplenishmentRequestGetFilterDto();
        var cacheKey = $"ReplenishmentRequestFilter:{filter.PartCodeContains}:{filter.RequestedQuantityEqual}:{filter.ReplenishmentStatusEqual}:{filter.RequestedAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var details = new ReplenishmentRequestReturnFilterDto();
        _cache.Setup(x => x.Get<ReplenishmentRequestReturnFilterDto>(cacheKey)).Returns((ReplenishmentRequestReturnFilterDto)null);
        _getFilter.Setup(x => x.GetFilter(filter)).Returns(details);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(details, result);
        _cache.Verify(x => x.Set(cacheKey, details, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void Create_ReturnsNull_IfValidationFails()
    {
        var dto = new ReplenishmentRequestCreateDto { PartCode = null };
        var decorator = CreateDecorator();
        var result = decorator.Create(dto);
        Assert.Null(result);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Create_MapsAndCreates()
    {
        var dto = new ReplenishmentRequestCreateDto
        {
            PartCode = "P123",
            RequestedQuantity = 10,
            ReplenishmentStatus = ReplenishmentStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        var entity = new ReplenishmentRequest { Id = Guid.NewGuid(), PartCode = dto.PartCode };
        var created = new ReplenishmentRequest { Id = Guid.NewGuid(), PartCode = dto.PartCode };
        var readDto = new ReplenishmentRequestReadDto { Id = created.Id, PartCode = dto.PartCode };

        _factory.Setup(x => x.MapToReplenishmentRequest(dto)).Returns(entity);
        _createCommand.Setup(x => x.Add(entity)).Returns(created);
        _factory.Setup(x => x.MapToReplenishmentRequestReadDto(created)).Returns(readDto);

        var decorator = CreateDecorator();
        var result = decorator.Create(dto);

        Assert.Equal(readDto, result);
    }

    [Fact]
    public void Update_DoesNothing_IfIdInvalid()
    {
        var dto = new ReplenishmentRequestUpdateDto { Id = Guid.Empty };
        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_DoesNothing_IfValidationFails()
    {
        var id = Guid.NewGuid();
        var entity = new ReplenishmentRequest { Id = id };
        var dto = new ReplenishmentRequestUpdateDto { Id = id, PartCode = null };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_MapsAndUpdatesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var entity = new ReplenishmentRequest { Id = id, PartCode = "P123" };
        var dto = new ReplenishmentRequestUpdateDto
        {
            Id = id,
            PartCode = "P123",
            RequestedQuantity = 10,
            ReplenishmentStatus = ReplenishmentStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);

        _factory.Verify(x => x.MapToReplenishmentRequestFromUpdateDto(entity, dto), Times.Once);
        _updateCommand.Verify(x => x.Update(entity), Times.Once);
        _cache.Verify(x => x.Remove($"ReplenishmentRequest:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("ReplenishmentRequestFilter:"), Times.Once);
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
        var entity = new ReplenishmentRequest { Id = id, PartCode = "P123" };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Delete(id);

        _deleteCommand.Verify(x => x.Delete(entity), Times.Once);
        _cache.Verify(x => x.Remove($"ReplenishmentRequest:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("ReplenishmentRequestFilter:"), Times.Once);
    }
}
