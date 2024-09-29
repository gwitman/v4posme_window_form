using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbParameter.Metadata))]
    public partial class TbParameter
    {
        public partial class Metadata
        {
    
            [Key]
            public object ParameterID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object IsRequiered { get; set; }
    
            public object IsEdited { get; set; }
        }
    }
}
