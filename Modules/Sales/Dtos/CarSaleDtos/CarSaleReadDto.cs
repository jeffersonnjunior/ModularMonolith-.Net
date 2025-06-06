﻿using Modules.Sales.Entities;
using Modules.Sales.Enums;

namespace Modules.Sales.Dtos.CarSaleDtos;

public class CarSaleReadDto
{
    public Guid Id { get; set; }
    public Guid ProductionOrderId { get; set; }
    public SaleStatus Status { get; set; }
    public DateTime? SoldAt { get; set; }

    public SaleDetail SaleDetail { get; set; }
}
