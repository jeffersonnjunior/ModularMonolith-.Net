namespace Modules.QualityInspectionModule.Dtos.InspectionDtos;

public class InspectionGetByIdDto
{
    public Guid Id { get; set; }
    public string[] Includes { get; set; }
}