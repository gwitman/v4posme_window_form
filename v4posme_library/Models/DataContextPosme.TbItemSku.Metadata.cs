using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemSku.Metadata))]
    public partial class TbItemSku
    {
        public partial class Metadata
        {
    
            [Key]
            public object SkuID { get; set; }
    
            public object ItemID { get; set; }
    
            public object CatalogItemID { get; set; }
    
            public object Value { get; set; }
    
            public object Price { get; set; }
    
            public object Predeterminado { get; set; }
        }
    }
}
