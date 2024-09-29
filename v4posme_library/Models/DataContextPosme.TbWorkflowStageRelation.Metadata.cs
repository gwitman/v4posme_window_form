using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbWorkflowStageRelation.Metadata))]
    public partial class TbWorkflowStageRelation
    {
        public partial class Metadata
        {
    
            [Key]
            public object WorkflowStageRelationID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object WorkflowID { get; set; }
    
            public object WorkflowStageID { get; set; }
    
            public object WorkflowStageTargetID { get; set; }
    
            public object NecesitaAuth { get; set; }
    
            public object AuthRolID { get; set; }
        }
    }
}
