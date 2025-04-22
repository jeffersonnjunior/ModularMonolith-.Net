using Modules.QualityInspectionModule.Entities;
using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Dtos.InspectionFailureDtos;

public class InspectionFailureCreateDto
{
    public Guid InspectionId { get; set; }
    public string Description { get; set; }
    public CorrectionActionType CorrectionActionType { get; set; }
    
    public Inspection Inspection { get; set; }
}