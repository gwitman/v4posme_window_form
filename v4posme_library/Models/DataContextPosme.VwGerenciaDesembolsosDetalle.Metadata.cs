using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwGerenciaDesembolsosDetalle.Metadata))]
    public partial class VwGerenciaDesembolsosDetalle
    {
        public partial class Metadata
        {
    
            public object Colaborador { get; set; }
    
            public object NombreColaborador { get; set; }
    
            public object Cliente { get; set; }
    
            public object NombreCliente { get; set; }
    
            public object Factura { get; set; }
    
            public object CreditAmortizationID { get; set; }
    
            public object FechaCuota { get; set; }
    
            public object AnoCuota { get; set; }
    
            public object Mes1Cuota { get; set; }
    
            public object Mes2Cuota { get; set; }
    
            public object CBalanceStartCuota { get; set; }
    
            public object CInteresCuota { get; set; }
    
            public object CCapitalCuota { get; set; }
    
            public object CBalanceEndCuota { get; set; }
    
            public object CShareCuota { get; set; }
    
            public object CRemainingCuota { get; set; }
    
            public object CshareCapital { get; set; }
    
            public object EstadoCuota { get; set; }
    
            public object DiasAtrazoCuota { get; set; }
    
            public object Moneda { get; set; }
    
            public object TipoCambioActual { get; set; }
    
            public object CCapitalPagado { get; set; }
    
            public object CCapitalPendiente { get; set; }
    
            public object CIntaresPagado { get; set; }
    
            public object CInteresPendiente { get; set; }
        }
    }
}
