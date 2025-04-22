namespace Modules.QualityInspectionModule.Dtos.InspectionFailureDtos;

public class InspectionFailureGetById
{
    public Guid Id { get; set; }
    public string[] Includes { get; set; }
}