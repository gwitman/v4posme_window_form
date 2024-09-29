using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwSinRiesgoReporteCredito.Metadata))]
    public partial class VwSinRiesgoReporteCredito
    {
        public partial class Metadata
        {
    
            public object CompanyID { get; set; }
    
            public object CustomerCreditDocumentID { get; set; }
    
            public object EntityID { get; set; }
    
            public object TIPODEENTIDAD { get; set; }
    
            public object NUMEROCORRELATIVO { get; set; }
    
            public object FECHADEREPORTE { get; set; }
    
            public object DEPARTAMENTO { get; set; }
    
            public object NUMERODECEDULAORUC { get; set; }
    
            public object NOMBREDEPERSONA { get; set; }
    
            public object TIPODECREDITO { get; set; }
    
            public object FECHADEDESEMBOLSO { get; set; }
    
            public object TIPODEOBLIGACION { get; set; }
    
            public object MONTOAUTORIZADO { get; set; }
    
            public object PLAZO { get; set; }
    
            public object FRECUENCIADEPAGO { get; set; }
    
            public object SALDODEUDA { get; set; }
    
            public object ESTADO { get; set; }
    
            public object MONTOVENCIDO { get; set; }
    
            public object ANTIGUEDADDEMORA { get; set; }
    
            public object TIPODEGARANTIA { get; set; }
    
            public object FORMADERECUPERACION { get; set; }
    
            public object NUMERODECREDITO { get; set; }
    
            public object VALORDELACUOTA { get; set; }
        }
    }
}
