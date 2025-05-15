using System;
using Common.Exceptions;
using Common.ICache.Services;
using Modules.Inventory.Decorators;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Interfaces.ICommands.IPartCommands;
using Modules.Inventory.Interfaces.IFactories;
using Modules.Inventory.Interfaces.IQuerys.IPartQuerys;
using Modules.Inventory.Entities;
using Moq;
using Xunit;
using Modules.Inventory;

namespace Tests.DecoratorsTests;

public class PartDecoratorTests
{
    private readonly Mock<IPartGetByElement> _getByElement = new();
    private readonly Mock<IPartGetFilter> _getFilter = new();
    private readonly Mock<IPartCreateCommand> _createCommand = new();
    private readonly Mock<IPartUpdateCommand> _updateCommand = new();
    private readonly Mock<IPartDeleteCommand> _deleteCommand = new();
    private readonly Mock<IPartFactory> _factory = new();
    private readonly Mock<ICacheService> _cache = new();
    private readonly NotificationContext _notificationContext = new();

    private PartDecorator CreateDecorator() =>
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
        _getByElement.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Part)null);
        var decorator = CreateDecorator();
        var result = decorator.GetById(Guid.NewGuid());
        Assert.Null(result);
    }

    [Fact]
    public void GetById_ReturnsFromCache_IfExists()
    {
        var id = Guid.NewGuid();
        var cached = new PartReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(new Part { Id = id });
        _cache.Setup(x => x.Get<PartReadDto>($"Part:{id}")).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetById_MapsAndCaches_WhenNotInCache()
    {
        var id = Guid.NewGuid();
        var entity = new Part { Id = id };
        var dto = new PartReadDto { Id = id };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);
        _cache.Setup(x => x.Get<PartReadDto>($"Part:{id}")).Returns((PartReadDto)null);
        _factory.Setup(x => x.MapToPartReadDto(entity)).Returns(dto);

        var decorator = CreateDecorator();
        var result = decorator.GetById(id);

        Assert.Equal(dto, result);
        _cache.Verify(x => x.Set($"Part:{id}", entity, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void GetFilter_ReturnsFromCache_IfExists()
    {
        var filter = new PartGetFilterDto();
        var cacheKey = $"PartFilter:{filter.CodeContains}:{filter.DescriptionContains}:{filter.QuantityInStockEqual}:{filter.MinimumRequiredEqual}:{filter.CreatedAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var cached = new PartReturnFilterDto();
        _cache.Setup(x => x.Get<PartReturnFilterDto>(cacheKey)).Returns(cached);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(cached, result);
    }

    [Fact]
    public void GetFilter_QueriesAndCaches_WhenNotInCache()
    {
        var filter = new PartGetFilterDto();
        var cacheKey = $"PartFilter:{filter.CodeContains}:{filter.DescriptionContains}:{filter.QuantityInStockEqual}:{filter.MinimumRequiredEqual}:{filter.CreatedAtEqual}:{filter.PageSize}:{filter.PageNumber}";
        var details = new PartReturnFilterDto();
        _cache.Setup(x => x.Get<PartReturnFilterDto>(cacheKey)).Returns((PartReturnFilterDto)null);
        _getFilter.Setup(x => x.GetFilter(filter)).Returns(details);

        var decorator = CreateDecorator();
        var result = decorator.GetFilter(filter);

        Assert.Equal(details, result);
        _cache.Verify(x => x.Set(cacheKey, details, It.IsAny<TimeSpan>()), Times.Once);
    }

    [Fact]
    public void Create_ReturnsNull_IfValidationFails()
    {
        var dto = new PartCreateDto { Code = null };
        var decorator = CreateDecorator();
        var result = decorator.Create(dto);
        Assert.Null(result);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Create_MapsAndCreates()
    {
        var dto = new PartCreateDto
        {
            Code = "C123",
            Description = "Test",
            QuantityInStock = 10,
            MinimumRequired = 1,
            CreatedAt = DateTime.UtcNow
        };
        var entity = new Part { Id = Guid.NewGuid(), Code = dto.Code };
        var created = new Part { Id = Guid.NewGuid(), Code = dto.Code };
        var readDto = new PartReadDto { Id = created.Id, Code = dto.Code };

        _factory.Setup(x => x.MapToPart(dto)).Returns(entity);
        _createCommand.Setup(x => x.Add(entity)).Returns(created);
        _factory.Setup(x => x.MapToPartReadDto(created)).Returns(readDto);

        var decorator = CreateDecorator();
        var result = decorator.Create(dto);

        Assert.Equal(readDto, result);
    }

    [Fact]
    public void Update_DoesNothing_IfIdInvalid()
    {
        var dto = new PartUpdateDto { Id = Guid.Empty };
        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_DoesNothing_IfValidationFails()
    {
        var id = Guid.NewGuid();
        var entity = new Part { Id = id };
        var dto = new PartUpdateDto { Id = id, Code = null };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);
        Assert.True(_notificationContext.HasNotifications());
    }

    [Fact]
    public void Update_MapsAndUpdatesAndRemovesCache()
    {
        var id = Guid.NewGuid();
        var entity = new Part { Id = id, Code = "C123" };
        var dto = new PartUpdateDto
        {
            Id = id,
            Code = "C123",
            Description = "Test",
            QuantityInStock = 10,
            MinimumRequired = 1,
            CreatedAt = DateTime.UtcNow
        };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Update(dto);

        _factory.Verify(x => x.MapToPartFromUpdateDto(entity, dto), Times.Once);
        _updateCommand.Verify(x => x.Update(entity), Times.Once);
        _cache.Verify(x => x.Remove($"Part:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("PartFilter:"), Times.Once);
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
        var entity = new Part { Id = id, Code = "C123" };
        _getByElement.Setup(x => x.GetById(id)).Returns(entity);

        var decorator = CreateDecorator();
        decorator.Delete(id);

        _deleteCommand.Verify(x => x.Delete(entity), Times.Once);
        _cache.Verify(x => x.Remove($"Part:{id}"), Times.Once);
        _cache.Verify(x => x.RemoveByPrefix("PartFilter:"), Times.Once);
    }
}
