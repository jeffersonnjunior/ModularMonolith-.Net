using Modules.QualityInspectionModule.Entities;
using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Dtos.InspectionFailureDtos;

public class InspectionFailureReadDto
{
    public Guid Id { get; set; }
    public Guid InspectionId { get; set; }
    public string Description { get; set; }
    public CorrectionActionType CorrectionActionType { get; set; }
    
    public Inspection Inspection { get; set; }
}