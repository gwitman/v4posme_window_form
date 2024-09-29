using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemImport.Metadata))]
    public partial class TbItemImport
    {
        public partial class Metadata
        {
    
            public object ItemNumber { get; set; }
    
            public object Fisico { get; set; }
    
            public object Sistema { get; set; }
        }
    }
}
