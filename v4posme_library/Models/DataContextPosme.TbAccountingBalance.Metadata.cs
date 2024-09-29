using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbAccountingBalance.Metadata))]
    public partial class TbAccountingBalance
    {
        public partial class Metadata
        {
    
            [Key]
            public object AccountBalanceID { get; set; }
    
            public object ComponentCycleID { get; set; }
    
            public object ComponentPeriodID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object AccountID { get; set; }
    
            public object BranchID { get; set; }
    
            public object Balance { get; set; }
    
            public object Debit { get; set; }
    
            public object Credit { get; set; }
    
            public object ClassID { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
