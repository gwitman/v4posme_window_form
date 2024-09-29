using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbSubelement.Metadata))]
    public partial class TbSubelement
    {
        public partial class Metadata
        {
    
            [Key]
            public object SubElementID { get; set; }
    
            public object ElementID { get; set; }
    
            public object Name { get; set; }
    
            public object WorkflowID { get; set; }
    
            public object CatalogID { get; set; }
        }
    }
}
