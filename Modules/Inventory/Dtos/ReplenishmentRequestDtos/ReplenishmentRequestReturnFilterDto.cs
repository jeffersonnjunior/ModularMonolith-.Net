namespace Modules.Inventory.Dtos.ReplenishmentRequestDtos;

public class ReplenishmentRequestReturnFilterDto
{
    public int TotalRegister { get; set; }

    public int TotalRegisterFilter { get; set; }

    public int TotalPages { get; set; }

    public List<ReplenishmentRequestReadDto> ReplenishmentRequestReadDto { get; set; }
}
