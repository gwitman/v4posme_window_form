using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbComponent.Metadata))]
    public partial class TbComponent
    {
        public partial class Metadata
        {
    
            [Key]
            public object ComponentID { get; set; }
    
            public object Name { get; set; }
        }
    }
}
