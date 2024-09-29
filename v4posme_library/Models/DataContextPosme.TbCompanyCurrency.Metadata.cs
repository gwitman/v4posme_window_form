using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyCurrency.Metadata))]
    public partial class TbCompanyCurrency
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyCurrencyID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Simb { get; set; }
        }
    }
}
