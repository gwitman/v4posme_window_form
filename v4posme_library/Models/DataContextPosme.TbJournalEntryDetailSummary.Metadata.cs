using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbJournalEntryDetailSummary.Metadata))]
    public partial class TbJournalEntryDetailSummary
    {
        public partial class Metadata
        {
    
            [Key]
            public object JournalEntryDetailSummaryID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object LoginID { get; set; }
    
            public object Tocken { get; set; }
    
            public object JournalEntryID { get; set; }
    
            public object AccountID { get; set; }
    
            public object ParentAccountID { get; set; }
    
            public object Debit { get; set; }
    
            public object Credit { get; set; }
        }
    }
}
