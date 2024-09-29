using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterSummaryConceptTmp.Metadata))]
    public partial class TbTransactionMasterSummaryConceptTmp
    {
        public partial class Metadata
        {
    
            [Key]
            public object ID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object LoginID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object TransactionMasterCausalID { get; set; }
    
            public object JournalEntryID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object TransactionNumber { get; set; }
    
            public object TransactionDate { get; set; }
    
            public object ExchangeRate { get; set; }
    
            public object ConceptID { get; set; }
    
            public object Value { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
        }
    }
}
