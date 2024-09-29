using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwContabilidadComprobante.Metadata))]
    public partial class VwContabilidadComprobante
    {
        public partial class Metadata
        {
    
            public object CodigoComprobante { get; set; }
    
            public object FechaComprobante { get; set; }
    
            public object TipoCambioComprobante { get; set; }
    
            public object EstadoComprobante { get; set; }
    
            public object DebitoComprobante { get; set; }
    
            public object CrditoComprobante { get; set; }
    
            public object TipoComprobante { get; set; }
    
            public object MonedaComprobante { get; set; }
    
            public object CentroCostoCuenta { get; set; }
    
            public object CodigoCuenta { get; set; }
    
            public object NombreCuenta { get; set; }
    
            public object DebitoCuenta { get; set; }
    
            public object CreditoCuenta { get; set; }
    
            public object TipoCuenta { get; set; }
    
            public object BeneficiarioComprobante { get; set; }
    
            public object NotaComprobante { get; set; }
        }
    }
}
