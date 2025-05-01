namespace Modules.Inventory.Dtos.PartDtos;

public class PartReturnFilterDto
{
    public int TotalRegister { get; set; }

    public int TotalRegisterFilter { get; set; }

    public int TotalPages { get; set; }

    public List<PartReadDto> Parts { get; set; }
}
