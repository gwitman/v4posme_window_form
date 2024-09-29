using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbComponentAudit.Metadata))]
    public partial class TbComponentAudit
    {
        public partial class Metadata
        {
    
            [Key]
            public object ComponentAuditID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object ElementID { get; set; }
    
            public object ElementItemID { get; set; }
    
            public object ModifiedOn { get; set; }
    
            public object ModifiedAt { get; set; }
    
            public object ModifiedIn { get; set; }
    
            public object ModifiedBy { get; set; }
        }
    }
}
