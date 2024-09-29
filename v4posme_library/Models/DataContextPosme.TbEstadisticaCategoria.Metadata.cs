using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEstadisticaCategoria.Metadata))]
    public partial class TbEstadisticaCategoria
    {
        public partial class Metadata
        {
    
            [Key]
            public object CategoriaID { get; set; }
    
            public object ClaseID { get; set; }
    
            public object Name { get; set; }
    
            public object StartValue { get; set; }
    
            public object EndValue { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
