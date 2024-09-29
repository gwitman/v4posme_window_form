using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerCredit.Metadata))]
    public partial class TbCustomerCredit
    {
        public partial class Metadata
        {
    
            [Key]
            public object CustomerCreditID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object LimitCreditDol { get; set; }
    
            public object BalanceDol { get; set; }
    
            public object IncomeDol { get; set; }
        }
    }
}
