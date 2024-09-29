using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterPurchase.Metadata))]
    public partial class TbTransactionMasterPurchase
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionMasterPurchaseID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object PurchaseTypeID { get; set; }
    
            public object TransportTypeID { get; set; }
        }
    }
}
