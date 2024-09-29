using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCustomerConsultasSinRiesgo.Metadata))]
    public partial class TbCustomerConsultasSinRiesgo
    {
        public partial class Metadata
        {
    
            [Key]
            public object RequestID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object Id { get; set; }
    
            public object File { get; set; }
    
            public object UserID { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object ModifiedOn { get; set; }
    
            public object IsPay { get; set; }
        }
    }
}
