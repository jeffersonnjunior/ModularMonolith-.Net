using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Production.Dtos.ProductionPartDtos;
using Modules.Production.Entities;
using Modules.Production.Querys.ProductionPartQuerys;
using Moq;

namespace Tests.QuerysTests;

public class ProductionPartQuerysTests
{
    [Fact]
    public void GetById_ShouldReturnProductionPart_WhenExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var partId = Guid.NewGuid();
        var part = new ProductionPart
        {
            Id = partId,
            ProductionOrderId = Guid.NewGuid(),
            PartCode = "P100",
            QuantityUsed = 5,
            ProductionOrder = null
        };
        mockRepo.Setup(r => r.Find<ProductionPart>(partId)).Returns(part);

        var query = new ProductionPartGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(partId);

        // Assert
        Assert.Equal(part, result);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var partId = Guid.NewGuid();
        mockRepo.Setup(r => r.Find<ProductionPart>(partId)).Returns((ProductionPart)null);

        var query = new ProductionPartGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(partId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetFilter_ShouldReturnFilteredParts()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var productionOrderId = Guid.NewGuid();
        var parts = new List<ProductionPart>
        {
            new ProductionPart
            {
                Id = Guid.NewGuid(),
                ProductionOrderId = productionOrderId,
                PartCode = "A1",
                QuantityUsed = 5,
                ProductionOrder = null
            },
            new ProductionPart
            {
                Id = Guid.NewGuid(),
                ProductionOrderId = Guid.NewGuid(),
                PartCode = "B2",
                QuantityUsed = 10,
                ProductionOrder = null
            }
        }.AsQueryable();

        mockRepo.Setup(r => r.Query<ProductionPart>()).Returns(parts);

        var query = new ProductionPartGetFilter(mockRepo.Object);

        var filter = new ProductionPartGetFilterDto
        {
            ProductionOrderIdEqual = productionOrderId,
            PartCodeContains = "A",
            QuantityUsedEqual = 5,
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = query.GetFilter(filter);

        // Assert
        Assert.Single(result.ProductionPart);
        Assert.Equal("A1", result.ProductionPart[0].PartCode);
        Assert.Equal(1, result.TotalRegisterFilter);
        Assert.Equal(1, result.TotalPages);
    }
}