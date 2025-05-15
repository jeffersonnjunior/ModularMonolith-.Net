using Common.IPersistence.IRepositories;
using Modules.Production.Commands.ProductionOrderCommands;
using Modules.Production.Entities;
using Modules.Production.Enums;
using Moq;

namespace Tests.CommandsTests;

public class ProductionOrderCommandsTests
{
    [Fact]
    public void CreateCommand_Create_ShouldCallAddAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var order = new ProductionOrder
        {
            Id = Guid.NewGuid(),
            Model = "ModelX",
            ProductionStatus = ProductionStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = null,
            Parts = new List<ProductionPart>()
        };
        mockRepo.Setup(r => r.Add(order)).Returns(order);

        var command = new ProductionOrderCreateCommand(mockRepo.Object);

        // Act
        var result = command.Create(order);

        // Assert
        mockRepo.Verify(r => r.Add(order), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        Assert.Equal(order, result);
    }

    [Fact]
    public void DeleteCommand_Delete_ShouldCallDeleteAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var order = new ProductionOrder
        {
            Id = Guid.NewGuid(),
            Model = "ModelY",
            ProductionStatus = ProductionStatus.InProduction,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = null,
            Parts = new List<ProductionPart>()
        };

        var command = new ProductionOrderDeleteCommand(mockRepo.Object);

        // Act
        command.Delete(order);

        // Assert
        mockRepo.Verify(r => r.Delete(order), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public void UpdateCommand_Update_ShouldCallUpdateAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var order = new ProductionOrder
        {
            Id = Guid.NewGuid(),
            Model = "ModelZ",
            ProductionStatus = ProductionStatus.Finished,
            CreatedAt = DateTime.UtcNow,
            CompletedAt = DateTime.UtcNow,
            Parts = new List<ProductionPart>()
        };

        var command = new ProductionOrderUpdateCommand(mockRepo.Object);

        // Act
        command.Update(order);

        // Assert
        mockRepo.Verify(r => r.Update(order), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }
}
