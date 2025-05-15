using Common.IPersistence.IRepositories;
using Modules.Sales.Commands.SaleDetailCommands;
using Modules.Sales.Entities;
using Moq;

namespace Tests.CommandsTests;

public class SaleDetailCommandsTests
{
    [Fact]
    public void CreateCommand_Create_ShouldCallAddAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var detail = new SaleDetail
        {
            Id = Guid.NewGuid(),
            CarSaleId = Guid.NewGuid(),
            BuyerName = "Jane Doe",
            Price = 80000,
            Discount = 2000,
            PaymentMethod = "Cartão",
            Notes = "Primeira parcela paga",
            CarSale = null
        };
        mockRepo.Setup(r => r.Add(detail)).Returns(detail);

        var command = new SaleDetailCreateCommand(mockRepo.Object);

        // Act
        var result = command.Create(detail);

        // Assert
        mockRepo.Verify(r => r.Add(detail), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        Assert.Equal(detail, result);
    }

    [Fact]
    public void DeleteCommand_Delete_ShouldCallDeleteAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var detail = new SaleDetail
        {
            Id = Guid.NewGuid(),
            CarSaleId = Guid.NewGuid(),
            BuyerName = "John Smith",
            Price = 90000,
            Discount = 1000,
            PaymentMethod = "Pix",
            Notes = "Venda à vista",
            CarSale = null
        };

        var command = new SaleDetailDeleteCommand(mockRepo.Object);

        // Act
        command.Delete(detail);

        // Assert
        mockRepo.Verify(r => r.Delete(detail), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }

    [Fact]
    public void UpdateCommand_Update_ShouldCallUpdateAndSaveChanges()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var detail = new SaleDetail
        {
            Id = Guid.NewGuid(),
            CarSaleId = Guid.NewGuid(),
            BuyerName = "Maria Silva",
            Price = 95000,
            Discount = 500,
            PaymentMethod = "Dinheiro",
            Notes = "Sem observações",
            CarSale = null
        };

        var command = new SaleDetailUpdateCommand(mockRepo.Object);

        // Act
        command.Update(detail);

        // Assert
        mockRepo.Verify(r => r.Update(detail), Times.Once);
        mockRepo.Verify(r => r.SaveChanges(), Times.Once);
    }
}
