using Common.IPersistence.IRepositories;
using Modules.Inventory;
using Modules.Inventory.Commands.PartCommands;
using Moq;

namespace Tests.CommandsTests;

public class PartCommandsTests
{
    [Fact]
    public void PartCreateCommand_Add_ShouldCallAddAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var part = new Part { Id = Guid.NewGuid(), Code = "P1", Description = "Test", QuantityInStock = 10, MinimumRequired = 1, CreatedAt = DateTime.UtcNow };
        mockRepo.Setup(r => r.Add(part)).Returns(part);

        var command = new PartCreateCommand(mockRepo.Object);

        // Act
        var result = command.Add(part);

        // Assert
        mockRepo.Verify(r => r.Add(part), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        Assert.Equal(part, result);
    }

    [Fact]
    public void PartDeleteCommand_Delete_ShouldCallDeleteAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var part = new Part { Id = Guid.NewGuid(), Code = "P2", Description = "Delete", QuantityInStock = 5, MinimumRequired = 2, CreatedAt = DateTime.UtcNow };

        var command = new PartDeleteCommand(mockRepo.Object);

        // Act
        command.Delete(part);

        // Assert
        mockRepo.Verify(r => r.Delete(part), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public void PartUpdateCommand_Update_ShouldCallUpdateAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var part = new Part { Id = Guid.NewGuid(), Code = "P3", Description = "Update", QuantityInStock = 7, MinimumRequired = 3, CreatedAt = DateTime.UtcNow };

        var command = new PartUpdateCommand(mockRepo.Object);

        // Act
        command.Update(part);

        // Assert
        mockRepo.Verify(r => r.Update(part), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }
}