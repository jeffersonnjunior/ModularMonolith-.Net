using Common.IPersistence.IRepositories;
using Modules.Production.Commands.ProductionPartCommands;
using Modules.Production.Entities;
using Moq;

namespace Tests.CommandsTests;

public class ProductionPartCommandsTests
{
    [Fact]
    public void CreateCommand_Create_ShouldCallAddAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var part = new ProductionPart
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.NewGuid(),
            PartCode = "P100",
            QuantityUsed = 5,
            ProductionOrder = null
        };
        mockRepo.Setup(r => r.Add(part)).Returns(part);

        var command = new ProductionPartCreateCommand(mockRepo.Object);

        // Act
        var result = command.Create(part);

        // Assert
        mockRepo.Verify(r => r.Add(part), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        Assert.Equal(part, result);
    }

    [Fact]
    public void DeleteCommand_Delete_ShouldCallDeleteAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var part = new ProductionPart
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.NewGuid(),
            PartCode = "P200",
            QuantityUsed = 3,
            ProductionOrder = null
        };

        var command = new ProductionPartDeleteCommand(mockRepo.Object);

        // Act
        command.Delete(part);

        // Assert
        mockRepo.Verify(r => r.Delete(part), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public void UpdateCommand_Update_ShouldCallUpdateAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var part = new ProductionPart
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.NewGuid(),
            PartCode = "P300",
            QuantityUsed = 7,
            ProductionOrder = null
        };

        var command = new ProductionPartUpdateCommand(mockRepo.Object);

        // Act
        command.Update(part);

        // Assert
        mockRepo.Verify(r => r.Update(part), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }
}
