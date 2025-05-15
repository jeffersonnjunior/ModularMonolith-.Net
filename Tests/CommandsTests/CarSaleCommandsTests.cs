using Common.IPersistence.IRepositories;
using Modules.Sales.Commands.CarSaleCommands;
using Modules.Sales.Entities;
using Modules.Sales.Enums;
using Moq;

namespace Tests.CommandsTests;

public class CarSaleCommandsTests
{
    [Fact]
    public void CreateCommand_Create_ShouldCallAddAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var sale = new CarSale
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.NewGuid(),
            Status = SaleStatus.Pendente,
            SoldAt = DateTime.UtcNow,
            SaleDetail = new SaleDetail
            {
                Id = Guid.NewGuid(),
                CarSaleId = Guid.NewGuid(),
                BuyerName = "John Doe",
                Price = 100000,
                Discount = 5000,
                PaymentMethod = "Pix",
                Notes = "Test",
                CarSale = null
            }
        };
        mockRepo.Setup(r => r.Add(sale)).Returns(sale);

        var command = new CarSaleCreateCommand(mockRepo.Object);

        // Act
        var result = command.Create(sale);

        // Assert
        mockRepo.Verify(r => r.Add(sale), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        Assert.Equal(sale, result);
    }

    [Fact]
    public void DeleteCommand_Delete_ShouldCallDeleteAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var sale = new CarSale
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.NewGuid(),
            Status = SaleStatus.Cancelada,
            SoldAt = null,
            SaleDetail = null
        };

        var command = new CarSaleDeleteCommand(mockRepo.Object);

        // Act
        command.Delete(sale);

        // Assert
        mockRepo.Verify(r => r.Delete(sale), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public void UpdateCommand_Update_ShouldCallUpdateAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var sale = new CarSale
        {
            Id = Guid.NewGuid(),
            ProductionOrderId = Guid.NewGuid(),
            Status = SaleStatus.Concluida,
            SoldAt = DateTime.UtcNow,
            SaleDetail = null
        };

        var command = new CarSaleUpdateCommand(mockRepo.Object);

        // Act
        command.Update(sale);

        // Assert
        mockRepo.Verify(r => r.Update(sale), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }
}
