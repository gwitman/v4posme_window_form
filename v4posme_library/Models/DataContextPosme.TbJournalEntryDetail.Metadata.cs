using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbJournalEntryDetail.Metadata))]
    public partial class TbJournalEntryDetail
    {
        public partial class Metadata
        {
    
            [Key]
            public object JournalEntryDetailID { get; set; }
    
            public object JournalEntryID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object AccountID { get; set; }
    
            public object IsActive { get; set; }
    
            public object ClassID { get; set; }
    
            public object Debit { get; set; }
    
            public object Credit { get; set; }
    
            public object Note { get; set; }
    
            public object IsApplied { get; set; }
    
            public object BranchID { get; set; }
    
            public object TbExchangeRate { get; set; }
        }
    }
}
