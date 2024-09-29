using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCatalogItem.Metadata))]
    public partial class TbCatalogItem
    {
        public partial class Metadata
        {
    
            [Key]
            public object CatalogItemID { get; set; }
    
            public object CatalogID { get; set; }
    
            public object Name { get; set; }
    
            public object Display { get; set; }
    
            public object FlavorID { get; set; }
    
            public object Description { get; set; }
    
            public object Sequence { get; set; }
    
            public object ParentCatalogID { get; set; }
    
            public object ParentCatalogItemID { get; set; }
    
            public object Ratio { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object Reference4 { get; set; }
        }
    }
}
