using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCatalog.Metadata))]
    public partial class TbCatalog
    {
        public partial class Metadata
        {
    
            [Key]
            public object CatalogID { get; set; }
    
            public object Name { get; set; }
    
            public object Orden { get; set; }
    
            public object Description { get; set; }
    
            public object IsActive { get; set; }
    
            public object PublicCatalogSystemName { get; set; }
        }
    }
}
