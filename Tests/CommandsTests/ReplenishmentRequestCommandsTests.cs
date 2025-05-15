using Common.IPersistence.IRepositories;
using Modules.Inventory.Commands.ReplenishmentRequestCommands;
using Modules.Inventory.Entities;
using Modules.Inventory.Enums;
using Moq;

namespace Tests.CommandsTests;

public class ReplenishmentRequestCommandsTests
{
    [Fact]
    public void CreateCommand_Add_ShouldCallAddAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var request = new ReplenishmentRequest
        {
            Id = Guid.NewGuid(),
            PartCode = "P001",
            RequestedQuantity = 10,
            ReplenishmentStatus = ReplenishmentStatus.Pending,
            RequestedAt = DateTime.UtcNow
        };
        mockRepo.Setup(r => r.Add(request)).Returns(request);

        var command = new ReplenishmentRequestCreateCommand(mockRepo.Object);

        // Act
        var result = command.Add(request);

        // Assert
        mockRepo.Verify(r => r.Add(request), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        Assert.Equal(request, result);
    }

    [Fact]
    public void DeleteCommand_Delete_ShouldCallDeleteAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var request = new ReplenishmentRequest
        {
            Id = Guid.NewGuid(),
            PartCode = "P002",
            RequestedQuantity = 5,
            ReplenishmentStatus = ReplenishmentStatus.InProgress,
            RequestedAt = DateTime.UtcNow
        };

        var command = new ReplenishmentRequestDeleteCommand(mockRepo.Object);

        // Act
        command.Delete(request);

        // Assert
        mockRepo.Verify(r => r.Delete(request), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public void UpdateCommand_Update_ShouldCallUpdateAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var request = new ReplenishmentRequest
        {
            Id = Guid.NewGuid(),
            PartCode = "P003",
            RequestedQuantity = 20,
            ReplenishmentStatus = ReplenishmentStatus.Completed,
            RequestedAt = DateTime.UtcNow
        };

        var command = new ReplenishmentRequestUpdateCommand(mockRepo.Object);

        // Act
        command.Update(request);

        // Assert
        mockRepo.Verify(r => r.Update(request), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }
}