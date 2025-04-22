using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Dtos.InspectionFailureDtos;

public class InspectionFailureGetFilter
{
    public Guid InspectionIdEqual { get; set; }
    public string DescriptionContains { get; set; }
    public CorrectionActionType CorrectionActionTypeEqual { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}