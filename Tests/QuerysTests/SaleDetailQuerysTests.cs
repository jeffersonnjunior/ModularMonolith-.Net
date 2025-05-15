using Common.Exceptions;
using Common.IPersistence.IRepositories;
using Modules.Sales.Dtos.SaleDetailDtos;
using Modules.Sales.Entities;
using Modules.Sales.Querys.SaleDetailQuerys;
using Moq;

namespace Tests.QuerysTests;

public class SaleDetailQuerysTests
{
    [Fact]
    public void GetById_ShouldReturnSaleDetail_WhenExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var detailId = Guid.NewGuid();
        var detail = new SaleDetail
        {
            Id = detailId,
            CarSaleId = Guid.NewGuid(),
            BuyerName = "Cliente 1",
            Price = 100000,
            Discount = 5000,
            PaymentMethod = "Pix",
            Notes = "Primeira parcela paga",
            CarSale = null
        };
        mockRepo.Setup(r => r.Find<SaleDetail>(detailId)).Returns(detail);

        var query = new SaleDetailGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(detailId);

        // Assert
        Assert.Equal(detail, result);
    }

    [Fact]
    public void GetById_ShouldReturnNull_WhenNotExists()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var notificationContext = new NotificationContext();
        var detailId = Guid.NewGuid();
        mockRepo.Setup(r => r.Find<SaleDetail>(detailId)).Returns((SaleDetail)null);

        var query = new SaleDetailGetByElement(mockRepo.Object, notificationContext);

        // Act
        var result = query.GetById(detailId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetFilter_ShouldReturnFilteredSaleDetails()
    {
        // Arrange
        var mockRepo = new Mock<IBaseRepository>();
        var carSaleId = Guid.NewGuid();
        var details = new List<SaleDetail>
        {
            new SaleDetail
            {
                Id = Guid.NewGuid(),
                CarSaleId = carSaleId,
                BuyerName = "Cliente 1",
                Price = 100000,
                Discount = 5000,
                PaymentMethod = "Pix",
                Notes = "Primeira parcela paga",
                CarSale = null
            },
            new SaleDetail
            {
                Id = Guid.NewGuid(),
                CarSaleId = Guid.NewGuid(),
                BuyerName = "Cliente 2",
                Price = 90000,
                Discount = 2000,
                PaymentMethod = "Cartão",
                Notes = "Venda à vista",
                CarSale = null
            }
        }.AsQueryable();

        mockRepo.Setup(r => r.Query<SaleDetail>()).Returns(details);

        var query = new SaleDetailGetFilter(mockRepo.Object);

        var filter = new SaleDetailGetFilterDto
        {
            CarSaleIdEqual = carSaleId,
            BuyerNameContains = "Cliente 1",
            PriceContainsEqual = 100000,
            DiscountEqual = 5000,
            PaymentMethodContains = "Pix",
            NotesContains = "Primeira parcela",
            PageNumber = 1,
            PageSize = 10
        };

        // Act
        var result = query.GetFilter(filter);

        // Assert
        Assert.Single(result.SaleDetailReadDto);
        Assert.Equal("Cliente 1", result.SaleDetailReadDto[0].BuyerName);
        Assert.Equal(1, result.TotalRegisterFilter);
        Assert.Equal(1, result.TotalPages);
    }
}