using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbComponentAutorization.Metadata))]
    public partial class TbComponentAutorization
    {
        public partial class Metadata
        {
    
            [Key]
            public object ComponentAutorizationID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
        }
    }
}
