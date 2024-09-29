using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCashBoxSession.Metadata))]
    public partial class TbCashBoxSession
    {
        public partial class Metadata
        {
    
            [Key]
            public object CashBoxSessionID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object CashBoxID { get; set; }
    
            public object StartOn { get; set; }
    
            public object EndOn { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsActive { get; set; }
    
            public object UserID { get; set; }
        }
    }
}
