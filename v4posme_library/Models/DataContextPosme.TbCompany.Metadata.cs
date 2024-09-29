using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompany.Metadata))]
    public partial class TbCompany
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object Address { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object IsActive { get; set; }
    
            public object FlavorID { get; set; }
    
            public object Type { get; set; }
        }
    }
}
