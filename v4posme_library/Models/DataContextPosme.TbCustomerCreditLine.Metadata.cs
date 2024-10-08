using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerCreditLine.Metadata))]
    public partial class TbCustomerCreditLine
    {
        public partial class Metadata
        {
    
            [Key]
            public object CustomerCreditLineID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object CreditLineID { get; set; }
    
            public object AccountNumber { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object TypeAmortization { get; set; }
    
            public object LimitCredit { get; set; }
    
            public object Balance { get; set; }
    
            public object InterestYear { get; set; }
    
            public object InterestPay { get; set; }
    
            public object TotalPay { get; set; }
    
            public object TotalDefeated { get; set; }
    
            public object DateOpen { get; set; }
    
            public object PeriodPay { get; set; }
    
            public object DateLastPay { get; set; }
    
            public object Term { get; set; }
    
            public object Note { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsActive { get; set; }
    
            public object DayExcluded { get; set; }
        }
    }
}
