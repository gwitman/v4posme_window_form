using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbWorkflowStageAffect.Metadata))]
    public partial class TbWorkflowStageAffect
    {
        public partial class Metadata
        {
    
            [Key]
            public object WorkflowStageAffectID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object FlavorID { get; set; }
    
            public object TransactionCausalID { get; set; }
    
            public object ComponentSourceID { get; set; }
    
            public object WorkflowSourceID { get; set; }
    
            public object WorkflowSourceStageID { get; set; }
    
            public object ComponentTargetID { get; set; }
    
            public object WorkflowTargetID { get; set; }
    
            public object WorkflowTargetStageID { get; set; }
    
            public object IsActive { get; set; }
    
            public object Condition1 { get; set; }
    
            public object Condition2 { get; set; }
    
            public object Condition3 { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
        }
    }
}
