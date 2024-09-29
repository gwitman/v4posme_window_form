using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterDetailCredit.Metadata))]
    public partial class TbTransactionMasterDetailCredit
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionMasterDetailCreditID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object TransactionMasterDetailID { get; set; }
    
            public object Capital { get; set; }
    
            public object Interest { get; set; }
    
            public object DayDalay { get; set; }
    
            public object InterestMora { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object ExchangeRate { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object Reference4 { get; set; }
    
            public object Reference5 { get; set; }
    
            public object Reference6 { get; set; }
    
            public object Reference7 { get; set; }
    
            public object Reference8 { get; set; }
    
            public object Reference9 { get; set; }
        }
    }
}
