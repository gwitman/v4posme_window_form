using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionCausal.Metadata))]
    public partial class TbTransactionCausal
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionCausalID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object BranchID { get; set; }
    
            public object Name { get; set; }
    
            public object WarehouseSourceID { get; set; }
    
            public object WarehouseTargetID { get; set; }
    
            public object IsDefault { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
