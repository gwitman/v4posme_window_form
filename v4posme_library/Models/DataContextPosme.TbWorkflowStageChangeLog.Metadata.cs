using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbWorkflowStageChangeLog.Metadata))]
    public partial class TbWorkflowStageChangeLog
    {
        public partial class Metadata
        {
    
            [Key]
            public object WorkflowStageChangeLogID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object WorkflowID { get; set; }
    
            public object WorkflowStageID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object Description { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedBy { get; set; }
        }
    }
}
