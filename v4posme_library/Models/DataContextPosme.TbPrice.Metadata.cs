using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbPrice.Metadata))]
    public partial class TbPrice
    {
        public partial class Metadata
        {
    
            [Key]
            public object PriceID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ListPriceID { get; set; }
    
            public object ItemID { get; set; }
    
            public object TypePriceID { get; set; }
    
            public object Percentage { get; set; }
    
            public object Price { get; set; }
    
            public object PercentageCommision { get; set; }
        }
    }
}
