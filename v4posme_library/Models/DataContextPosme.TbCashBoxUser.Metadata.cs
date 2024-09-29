using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCashBoxUser.Metadata))]
    public partial class TbCashBoxUser
    {
        public partial class Metadata
        {
    
            [Key]
            public object CashBoxUserID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object UserID { get; set; }
    
            public object CashBoxID { get; set; }
    
            public object TypeID { get; set; }
        }
    }
}
