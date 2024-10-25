using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterDetailReference.Metadata))]
    public partial class TbTransactionMasterDetailReference
    {
        public partial class Metadata
        {
    
            public object TransactionMasterDetailID { get; set; }
    
            [Key]
            public object TransactionMasterDetailRefereceID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object Quantity { get; set; }
    
            public object IsActive { get; set; }
    
            public object CreatedOn { get; set; }
        }
    }
}
