using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Production.Dtos.ProductionOrderDtos;
using Modules.Production.Entities;
using Modules.Production.Enums;
using Modules.Production.Querys.ProductionOrderQuerys;
using Moq;

namespace Tests.QuerysTests;

public class ProductionOrderQuerysTests
{
    [Fact]
    public void GetById_ShouldReturnProductionOrder_WhenExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var orderId = Guid.NewGuid();
        var order = new ProductionOrder
        {
            Id = orderId,
            Model = "ModelX",
            ProductionStatus = ProductionStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = null,
            Parts = new List<ProductionPart>()
        };
        mockRepo.Setup(r => r.Find<ProductionOrder>(orderId)).Returns(order);

        var query = new ProductionOrderGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(orderId);

        // Assert
        Assert.Equal(order, result);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var orderId = Guid.NewGuid();
        mockRepo.Setup(r => r.Find<ProductionOrder>(orderId)).Returns((ProductionOrder)null);

        var query = new ProductionOrderGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(orderId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetFilter_ShouldReturnFilteredOrders()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var orders = new List<ProductionOrder>
        {
            new ProductionOrder
            {
                Id = Guid.NewGuid(),
                Model = "A1",
                ProductionStatus = ProductionStatus.Pending,
                CreatedAt = DateTime.Today,
                CompletedAt = null,
                Parts = new List<ProductionPart>()
            },
            new ProductionOrder
            {
                Id = Guid.NewGuid(),
                Model = "B2",
                ProductionStatus = ProductionStatus.Finished,
                CreatedAt = DateTime.Today,
                CompletedAt = DateTime.Today,
                Parts = new List<ProductionPart>()
            }
        }.AsQueryable();

        mockRepo.Setup(r => r.Query<ProductionOrder>()).Returns(orders);

        var query = new ProductionOrderGetFilter(mockRepo.Object);

        var filter = new ProductionOrderGetFilterDto
        {
            ModelContains = "A",
            ProductionStatusEqual = ProductionStatus.Pending,
            CreatedAtEqual = DateTime.Today,
            CompletedAtEqual = null,
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = query.GetFilter(filter);

        // Assert
        Assert.Single(result.ProductionOrder);
        Assert.Equal("A1", result.ProductionOrder[0].Model);
        Assert.Equal(1, result.TotalRegisterFilter);
        Assert.Equal(1, result.TotalPages);
    }
}
