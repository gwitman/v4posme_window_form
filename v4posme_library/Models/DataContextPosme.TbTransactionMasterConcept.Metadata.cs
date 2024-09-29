using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterConcept.Metadata))]
    public partial class TbTransactionMasterConcept
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionMasterConceptID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object ConceptID { get; set; }
    
            public object Value { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object ExchangeRate { get; set; }
        }
    }
}
