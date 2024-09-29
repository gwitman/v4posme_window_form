using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwGerenciaEstadoResultado001.Metadata))]
    public partial class VwGerenciaEstadoResultado001
    {
        public partial class Metadata
        {
    
            public object Cuenta { get; set; }
    
            public object Ano { get; set; }
    
            public object Mes { get; set; }
    
            public object MesOnly { get; set; }
    
            public object CsaldoInicial { get; set; }
    
            public object CsaldoFinal { get; set; }
    
            public object CsaldoMensual { get; set; }
        }
    }
}
