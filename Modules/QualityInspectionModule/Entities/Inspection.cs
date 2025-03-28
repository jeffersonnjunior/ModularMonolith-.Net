using Modules.ProductionInventoryModule.Entities;
using Modules.QualityInspectionModule.Enums;

namespace Modules.QualityInspectionModule.Entities;

public class Inspection
{
    public Guid Id { get; set; }
    public Guid ProductionStageId { get; set; }
    public DateTime InspectionDate { get; set; }
    public InspectionResult InspectionResult { get; set; }
    public string Notes { get; set; }
    
    public ProductionStage ProductionStage { get; set; }
    public ICollection<InspectionFailure> InspectionFailures { get; set; }
}