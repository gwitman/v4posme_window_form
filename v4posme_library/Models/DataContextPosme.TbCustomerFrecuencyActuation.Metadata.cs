using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerFrecuencyActuation.Metadata))]
    public partial class TbCustomerFrecuencyActuation
    {
        public partial class Metadata
        {
    
            [Key]
            public object CustomerFrecuencyActuations { get; set; }
    
            public object EntityID { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object Name { get; set; }
    
            public object SituationID { get; set; }
    
            public object FrecuencyContactID { get; set; }
    
            public object IsActive { get; set; }
    
            public object IsApply { get; set; }
        }
    }
}
