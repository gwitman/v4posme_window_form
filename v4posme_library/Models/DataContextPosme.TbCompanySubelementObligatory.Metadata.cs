using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanySubelementObligatory.Metadata))]
    public partial class TbCompanySubelementObligatory
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanySubelementObligatoryID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ElementID { get; set; }
    
            public object SubElementID { get; set; }
        }
    }
}
