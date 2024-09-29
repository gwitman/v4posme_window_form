using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbItemConfigLoto.Metadata))]
    public partial class TbItemConfigLoto
    {
        public partial class Metadata
        {
    
            [Key]
            public object ItemConfigLotoID { get; set; }
    
            public object IsActive { get; set; }
    
            public object MaxSale { get; set; }
    
            public object Turno1Inicio { get; set; }
    
            public object Turno1Fin { get; set; }
    
            public object Turno2Inicio { get; set; }
    
            public object Turno2Fin { get; set; }
    
            public object Turno3Inicio { get; set; }
    
            public object Turno3Fin { get; set; }
    
            public object ItemID { get; set; }
        }
    }
}
