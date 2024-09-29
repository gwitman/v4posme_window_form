using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwGerenciaDesembolsosResuman.Metadata))]
    public partial class VwGerenciaDesembolsosResuman
    {
        public partial class Metadata
        {
    
            public object CodigoCliente { get; set; }
    
            public object Nombre { get; set; }
    
            public object Moneda { get; set; }
    
            public object Edad { get; set; }
    
            public object CMonto { get; set; }
    
            public object CBalance { get; set; }
    
            public object CProvisionado { get; set; }
    
            public object Estado { get; set; }
    
            public object Interes { get; set; }
    
            public object Plazo { get; set; }
    
            public object TipoCambio { get; set; }
    
            public object Fecha { get; set; }
    
            public object TipoAmortizacion { get; set; }
    
            public object PeriodoPago { get; set; }
    
            public object Anio { get; set; }
    
            public object Mes { get; set; }
    
            public object MesUnicamente { get; set; }
    
            public object Factura { get; set; }
        }
    }
}
