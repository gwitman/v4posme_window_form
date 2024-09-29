using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbJournalEntry.Metadata))]
    public partial class TbJournalEntry
    {
        public partial class Metadata
        {
    
            [Key]
            public object JournalEntryID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object JournalNumber { get; set; }
    
            public object JournalDate { get; set; }
    
            public object TbExchangeRate { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object IsActive { get; set; }
    
            public object IsApplied { get; set; }
    
            public object TitleTemplated { get; set; }
    
            public object IsTemplated { get; set; }
    
            public object StatusID { get; set; }
    
            public object Note { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object Debit { get; set; }
    
            public object Credit { get; set; }
    
            public object JournalTypeID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object AccountingCycleID { get; set; }
    
            public object EntryName { get; set; }
    
            public object IsModule { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object TransactionID { get; set; }
        }
    }
}
