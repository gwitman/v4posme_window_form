using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbComponentAuditDetail.Metadata))]
    public partial class TbComponentAuditDetail
    {
        public partial class Metadata
        {
    
            [Key]
            public object ComponentAuditDetailID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object ComponentAuditID { get; set; }
    
            public object FieldID { get; set; }
    
            public object OldValue { get; set; }
    
            public object NewValue { get; set; }
    
            public object Note { get; set; }
        }
    }
}
