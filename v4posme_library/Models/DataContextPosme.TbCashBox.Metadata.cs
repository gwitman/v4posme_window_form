using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCashBox.Metadata))]
    public partial class TbCashBox
    {
        public partial class Metadata
        {
    
            [Key]
            public object CashBoxID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object CashBoxCode { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
