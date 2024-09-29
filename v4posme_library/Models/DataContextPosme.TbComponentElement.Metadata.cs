using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbComponentElement.Metadata))]
    public partial class TbComponentElement
    {
        public partial class Metadata
        {
    
            [Key]
            public object ComponentElementID { get; set; }
    
            public object ElementID { get; set; }
    
            public object ComponentID { get; set; }
        }
    }
}
