using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEstadisticaClase.Metadata))]
    public partial class TbEstadisticaClase
    {
        public partial class Metadata
        {
    
            [Key]
            public object ClaseID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
