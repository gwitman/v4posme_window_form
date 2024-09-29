using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCatalogItemConvertion.Metadata))]
    public partial class TbCatalogItemConvertion
    {
        public partial class Metadata
        {
    
            [Key]
            public object CatalogItemConvertionID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object CatalogID { get; set; }
    
            public object CatalogItemID { get; set; }
    
            public object TargetCatalogItemID { get; set; }
    
            public object Ratio { get; set; }
    
            public object RegisterDate { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
