using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbComponentAutorizationDetail.Metadata))]
    public partial class TbComponentAutorizationDetail
    {
        public partial class Metadata
        {
    
            [Key]
            public object ComponentAurotizationDetailID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentAutorizationID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object WorkflowID { get; set; }
    
            public object WorkflowStageID { get; set; }
        }
    }
}
