using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerCreditClasification.Metadata))]
    public partial class TbCustomerCreditClasification
    {
        public partial class Metadata
        {
    
            [Key]
            public object ClasificationID { get; set; }
    
            public object EntityID { get; set; }
    
            public object DateHistory { get; set; }
    
            public object NumberShareLate { get; set; }
    
            public object AmountCapitalLate { get; set; }
    
            public object AmountInterestLate { get; set; }
    
            public object MaxDayMora { get; set; }
    
            public object NumberCreditAbiertos { get; set; }
    
            public object NumberCreditSaneados { get; set; }
    
            public object NumberCreditCancelados { get; set; }
    
            public object AmountCapitalAbierto { get; set; }
    
            public object AmountCapitalSaneado { get; set; }
    
            public object AmountCapitalCancelado { get; set; }
    
            public object Summary { get; set; }
        }
    }
}
