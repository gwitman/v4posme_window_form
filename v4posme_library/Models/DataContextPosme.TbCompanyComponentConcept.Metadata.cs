using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyComponentConcept.Metadata))]
    public partial class TbCompanyComponentConcept
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyComponentConceptID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object Name { get; set; }
    
            public object ValueIn { get; set; }
    
            public object ValueOut { get; set; }
        }
    }
}
