using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTypeMenuElement.Metadata))]
    public partial class TbTypeMenuElement
    {
        public partial class Metadata
        {
    
            [Key]
            public object TypeMenuElementID { get; set; }
    
            public object Name { get; set; }
        }
    }
}
