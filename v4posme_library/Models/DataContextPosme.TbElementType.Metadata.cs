using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbElementType.Metadata))]
    public partial class TbElementType
    {
        public partial class Metadata
        {
    
            [Key]
            public object ElementTypeID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
        }
    }
}
