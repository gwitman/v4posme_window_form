using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterDenomination.Metadata))]
    public partial class TbTransactionMasterDenomination
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionMasterDenominationID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object IsActive { get; set; }
    
            public object ComponentID { get; set; }
    
            public object CatalogItemID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object ExchangeRate { get; set; }
    
            public object Quantity { get; set; }
    
            public object Ratio { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
        }
    }
}
