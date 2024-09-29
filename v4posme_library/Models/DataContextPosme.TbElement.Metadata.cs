using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbElement.Metadata))]
    public partial class TbElement
    {
        public partial class Metadata
        {
    
            [Key]
            public object ElementID { get; set; }
    
            public object ElementTypeID { get; set; }
    
            public object Name { get; set; }
    
            public object ColumnAutoIncrement { get; set; }
        }
    }
}
