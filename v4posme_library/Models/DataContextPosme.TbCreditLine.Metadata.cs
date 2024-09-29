using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCreditLine.Metadata))]
    public partial class TbCreditLine
    {
        public partial class Metadata
        {
    
            [Key]
            public object CreditLineID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
