using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Dtos.InspectionDtos;

public class InspectionGetFilterDto
{
    public Guid ProductionStageIdEqual { get; set; }
    public DateTime InspectionDateEqual { get; set; }
    public InspectionResult InspectionResultEqual { get; set; }
    public string NotesContains { get; set; }
}