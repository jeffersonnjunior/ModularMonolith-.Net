using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Dtos.InspectionFailureDtos;

public class InspectionFailureUpdateDto
{
    public Guid Id { get; set; }
    public Guid InspectionId { get; set; }
    public string Description { get; set; }
    public CorrectionActionType CorrectionActionType { get; set; }
}