using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwGerenciaEstadoResultado002.Metadata))]
    public partial class VwGerenciaEstadoResultado002
    {
        public partial class Metadata
        {
    
            public object Ano { get; set; }
    
            public object Mes { get; set; }
    
            public object MesOnly { get; set; }
    
            public object CsaldoInicial { get; set; }
    
            public object CsaldoFinal { get; set; }
    
            public object CsaldoMensual { get; set; }
        }
    }
}
