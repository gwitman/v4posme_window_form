using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEntityPhone.Metadata))]
    public partial class TbEntityPhone
    {
        public partial class Metadata
        {
    
            [Key]
            public object EntityPhoneID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object TypeID { get; set; }
    
            public object Number { get; set; }
    
            public object IsPrimary { get; set; }
        }
    }
}
