using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory;
using Modules.Inventory.Dtos.PartDtos;
using Modules.Inventory.Querys.PartQuerys;
using Moq;

namespace Tests.QuerysTests;

public class PartQuerysTests
{
    [Fact]
    public void GetById_ShouldReturnPart_WhenPartExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var partId = Guid.NewGuid();
        var part = new Part
        {
            Id = partId,
            Code = "P001",
            Description = "Test Part",
            QuantityInStock = 10,
            MinimumRequired = 2,
            CreatedAt = DateTime.UtcNow
        };
        mockRepo.Setup(r => r.Find<Part>(partId)).Returns(part);

        var query = new PartGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(partId);

        // Assert
        Assert.Equal(part, result);
    }

    [Fact]
    public void GetFilter_ShouldReturnFilteredParts()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var parts = new List<Part>
        {
            new Part { Id = Guid.NewGuid(), Code = "A1", Description = "Desc1", QuantityInStock = 5, MinimumRequired = 2, CreatedAt = DateTime.Today },
            new Part { Id = Guid.NewGuid(), Code = "B2", Description = "Desc2", QuantityInStock = 10, MinimumRequired = 3, CreatedAt = DateTime.Today }
        }.AsQueryable();

        mockRepo.Setup(r => r.Query<Part>()).Returns(parts);

        var query = new PartGetFilter(mockRepo.Object);

        var filter = new PartGetFilterDto
        {
            CodeContains = "A",
            DescriptionContains = "Desc1",
            QuantityInStockEqual = 5,
            MinimumRequiredEqual = 2,
            CreatedAtEqual = DateTime.Today,
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = query.GetFilter(filter);

        // Assert
        Assert.Single(result.Parts);
        Assert.Equal("A1", result.Parts[0].Code);
        Assert.Equal(1, result.TotalRegisterFilter);
        Assert.Equal(1, result.TotalPages);
    }
}
