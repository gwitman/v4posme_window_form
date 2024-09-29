using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransaction.Metadata))]
    public partial class TbTransaction
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object WorkflowID { get; set; }
    
            public object IsCountable { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object GenerateTransactionNumber { get; set; }
    
            public object DecimalPlaces { get; set; }
    
            public object JournalTypeID { get; set; }
    
            public object SignInventory { get; set; }
    
            public object IsRevert { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
