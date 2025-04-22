using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Dtos.InspectionDtos;

public class InspectionCreateDto
{
    public Guid ProductionStageId { get; set; }
    public DateTime InspectionDate { get; set; }
    public InspectionResult InspectionResult { get; set; }
    public string Notes { get; set; }
}