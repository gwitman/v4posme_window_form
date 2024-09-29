using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCashBoxSessionTransactionMaster.Metadata))]
    public partial class TbCashBoxSessionTransactionMaster
    {
        public partial class Metadata
        {
    
            [Key]
            public object CashBoxSessionTransactionMasterID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object CashBoxID { get; set; }
    
            public object CashBoxSessionID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
        }
    }
}
