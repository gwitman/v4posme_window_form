using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(VwGerenciaCustomer.Metadata))]
    public partial class VwGerenciaCustomer
    {
        public partial class Metadata
        {
    
            public object CustomerNumber { get; set; }
    
            public object FirstName { get; set; }
    
            public object Identification { get; set; }
    
            public object BirthDate { get; set; }
        }
    }
}
