using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerCreditDocument.Metadata))]
    public partial class TbCustomerCreditDocument
    {
        public partial class Metadata
        {
    
            [Key]
            public object CustomerCreditDocumentID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object EntityID { get; set; }
    
            public object CustomerCreditLineID { get; set; }
    
            public object DocumentNumber { get; set; }
    
            public object DateOn { get; set; }
    
            public object Amount { get; set; }
    
            public object Interes { get; set; }
    
            public object Term { get; set; }
    
            public object Balance { get; set; }
    
            public object BalanceProvicioned { get; set; }
    
            public object ExchangeRate { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object StatusID { get; set; }
    
            public object IsActive { get; set; }
    
            public object TypeAmortization { get; set; }
    
            public object PeriodPay { get; set; }
    
            public object ProviderIDCredit { get; set; }
    
            public object ReportSinRiesgo { get; set; }
        }
    }
}
