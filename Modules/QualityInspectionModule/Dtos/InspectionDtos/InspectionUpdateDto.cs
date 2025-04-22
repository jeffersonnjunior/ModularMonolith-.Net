using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Dtos.InspectionDtos;

public class InspectionUpdateDto
{
    public Guid Id { get; set; }
    public Guid ProductionStageId { get; set; }
    public DateTime InspectionDate { get; set; }
    public InspectionResult InspectionResult { get; set; }
    public string Notes { get; set; }
}