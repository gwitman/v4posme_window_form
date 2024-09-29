using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCurrency.Metadata))]
    public partial class TbCurrency
    {
        public partial class Metadata
        {
    
            [Key]
            public object CurrencyID { get; set; }
    
            public object Simbol { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
