using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwGerenciaBalance.Metadata))]
    public partial class VwGerenciaBalance
    {
        public partial class Metadata
        {
    
            public object CentroCosto { get; set; }
    
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
