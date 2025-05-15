using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Inventory.Dtos.ReplenishmentRequestDtos;
using Modules.Inventory.Entities;
using Modules.Inventory.Enums;
using Modules.Inventory.Querys.ReplenishmentRequestQuerys;
using Moq;

namespace Tests.QuerysTests;

public class ReplenishmentRequestQuerysTests
{
    [Fact]
    public void GetById_ShouldReturnRequest_WhenRequestExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var requestId = Guid.NewGuid();
        var request = new ReplenishmentRequest
        {
            Id = requestId,
            PartCode = "PC001",
            RequestedQuantity = 5,
            ReplenishmentStatus = ReplenishmentStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        mockRepo.Setup(r => r.Find<ReplenishmentRequest>(requestId)).Returns(request);

        var query = new ReplenishmentRequestGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(requestId);

        // Assert
        Assert.Equal(request, result);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenRequestDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var requestId = Guid.NewGuid();
        mockRepo.Setup(r => r.Find<ReplenishmentRequest>(requestId)).Returns((ReplenishmentRequest)null);

        var query = new ReplenishmentRequestGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(requestId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetFilter_ShouldReturnFilteredRequests()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var requests = new List<ReplenishmentRequest>
        {
            new ReplenishmentRequest
            {
                Id = Guid.NewGuid(),
                PartCode = "A1",
                RequestedQuantity = 5,
                ReplenishmentStatus = ReplenishmentStatus.Pending,
                RequestedAt = DateTime.Today
            },
            new ReplenishmentRequest
            {
                Id = Guid.NewGuid(),
                PartCode = "B2",
                RequestedQuantity = 10,
                ReplenishmentStatus = ReplenishmentStatus.Completed,
                RequestedAt = DateTime.Today
            }
        }.AsQueryable();

        mockRepo.Setup(r => r.Query<ReplenishmentRequest>()).Returns(requests);

        var query = new ReplenishmentRequestGetFilter(mockRepo.Object);

        var filter = new ReplenishmentRequestGetFilterDto
        {
            PartCodeContains = "A",
            RequestedQuantityEqual = 5,
            ReplenishmentStatusEqual = ReplenishmentStatus.Pending,
            RequestedAtEqual = DateTime.Today,
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = query.GetFilter(filter);

        // Assert
        Assert.Single(result.ReplenishmentRequestReadDto);
        Assert.Equal("A1", result.ReplenishmentRequestReadDto[0].PartCode);
        Assert.Equal(1, result.TotalRegisterFilter);
        Assert.Equal(1, result.TotalPages);
    }
}
