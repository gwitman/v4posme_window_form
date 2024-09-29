using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbPublicCatalog.Metadata))]
    public partial class TbPublicCatalog
    {
        public partial class Metadata
        {
    
            [Key]
            public object PublicCatalogID { get; set; }
    
            public object Name { get; set; }
    
            public object SystemName { get; set; }
    
            public object StatusID { get; set; }
    
            public object Orden { get; set; }
    
            public object Description { get; set; }
    
            public object IsActive { get; set; }
    
            public object FlavorID { get; set; }
        }
    }
}
