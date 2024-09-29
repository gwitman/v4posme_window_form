using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerCreditAmoritization.Metadata))]
    public partial class TbCustomerCreditAmoritization
    {
        public partial class Metadata
        {
    
            [Key]
            public object CreditAmortizationID { get; set; }
    
            public object CustomerCreditDocumentID { get; set; }
    
            public object DateApply { get; set; }
    
            public object BalanceStart { get; set; }
    
            public object Interest { get; set; }
    
            public object Capital { get; set; }
    
            public object Share { get; set; }
    
            public object BalanceEnd { get; set; }
    
            public object Remaining { get; set; }
    
            public object ShareCapital { get; set; }
    
            public object DayDelay { get; set; }
    
            public object Note { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
