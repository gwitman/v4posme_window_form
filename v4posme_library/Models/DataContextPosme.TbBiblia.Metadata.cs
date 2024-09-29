using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbBiblia.Metadata))]
    public partial class TbBiblia
    {
        public partial class Metadata
        {
    
            [Key]
            public object VersiculoID { get; set; }
    
            public object Orden { get; set; }
    
            public object Dia { get; set; }
    
            public object Capitulo { get; set; }
    
            public object Libro { get; set; }
    
            public object Versiculo { get; set; }
        }
    }
}
