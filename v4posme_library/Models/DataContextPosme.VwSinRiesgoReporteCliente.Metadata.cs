using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwSinRiesgoReporteCliente.Metadata))]
    public partial class VwSinRiesgoReporteCliente
    {
        public partial class Metadata
        {
    
            public object FECHAREPORTE { get; set; }
    
            public object IDENTIFICACION { get; set; }
    
            public object TIPODEPERSONA { get; set; }
    
            public object NACIONALIDAD { get; set; }
    
            public object SEXO { get; set; }
    
            public object FECHADENACIMIENTO { get; set; }
    
            public object ESTADOCIVIL { get; set; }
    
            public object DIRECCION { get; set; }
    
            public object DEPARTAMENTO { get; set; }
    
            public object MUNICIPIO { get; set; }
    
            public object DIRECCIONDETRABAJO { get; set; }
    
            public object DEPARTAMENTODETRABAJO { get; set; }
    
            public object MUNICIPIODETRABAJO { get; set; }
    
            public object TELEFONODOMICILIAR { get; set; }
    
            public object TELEFONOTRABAJO { get; set; }
    
            public object CELULAR { get; set; }
    
            public object CORREOELECTRONICO { get; set; }
    
            public object OCUPACION { get; set; }
    
            public object ACTIVIDADECONOMICA { get; set; }
    
            public object SECTOR { get; set; }
        }
    }
}
