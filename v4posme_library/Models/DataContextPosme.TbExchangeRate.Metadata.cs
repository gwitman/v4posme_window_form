using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbExchangeRate.Metadata))]
    public partial class TbExchangeRate
    {
        public partial class Metadata
        {
    
            [Key]
            public object ExchangeRateID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Date { get; set; }
    
            public object TargetCurrencyID { get; set; }
    
            public object Ratio { get; set; }
    
            public object Value { get; set; }
        }
    }
}
