using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbNaturale.Metadata))]
    public partial class TbNaturale
    {
        public partial class Metadata
        {
    
            [Key]
            public object NaturalesID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object EntityID { get; set; }
    
            public object FirstName { get; set; }
    
            public object LastName { get; set; }
    
            public object Address { get; set; }
    
            public object StatusID { get; set; }
    
            public object ProfesionID { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
