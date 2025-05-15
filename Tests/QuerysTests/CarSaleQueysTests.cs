using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Sales.Entities;
using Modules.Sales.Enums;
using Modules.Sales.Querys.CarSaleQuerys;
using Modules.Sales.Querys.CarSaleQueys;
using Moq;

namespace Tests.QuerysTests;

public class CarSaleQueysTests
{
    [Fact]
    public void GetById_ShouldReturnCarSale_WhenExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var saleId = Guid.NewGuid();
        var sale = new CarSale
        {
            Id = saleId,
            ProductionOrderId = Guid.NewGuid(),
            Status = SaleStatus.Pendente,
            SoldAt = DateTime.UtcNow,
            SaleDetail = null
        };
        mockRepo.Setup(r => r.Find<CarSale>(saleId)).Returns(sale);

        var query = new CarSaleGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(saleId);

        // Assert
        Assert.Equal(sale, result);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var saleId = Guid.NewGuid();
        mockRepo.Setup(r => r.Find<CarSale>(saleId)).Returns((CarSale)null);

        var query = new CarSaleGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(saleId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetFilter_ShouldReturnFilteredCarSales()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var productionOrderId = Guid.NewGuid();
        var sales = new List<CarSale>
        {
            new CarSale
            {
                Id = Guid.NewGuid(),
                ProductionOrderId = productionOrderId,
                Status = SaleStatus.Concluida,
                SoldAt = DateTime.Today,
                SaleDetail = null
            },
            new CarSale
            {
                Id = Guid.NewGuid(),
                ProductionOrderId = Guid.NewGuid(),
                Status = SaleStatus.Cancelada,
                SoldAt = DateTime.Today,
                SaleDetail = null
            }
        }.AsQueryable();

        mockRepo.Setup(r => r.Query<CarSale>()).Returns(sales);

        var query = new CarSaleGetFilter(mockRepo.Object);

        var filter = new CarSaleGetFilterDto
        {
            ProductionOrderIdEqual = productionOrderId,
            StatusEqual = SaleStatus.Concluida,
            SoldAtEqual = DateTime.Today,
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = query.GetFilter(filter);

        // Assert
        Assert.Single(result.CarSaleReadDto);
        Assert.Equal(SaleStatus.Concluida, result.CarSaleReadDto[0].Status);
        Assert.Equal(1, result.TotalRegisterFilter);
        Assert.Equal(1, result.TotalPages);
    }
}
