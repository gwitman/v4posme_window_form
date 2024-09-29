using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbRazonesFinancierasTmp.Metadata))]
    public partial class TbRazonesFinancierasTmp
    {
        public partial class Metadata
        {
    
            [Key]
            public object RzID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object LoginID { get; set; }
    
            public object Token { get; set; }
    
            public object Name { get; set; }
    
            public object Sequence { get; set; }
    
            public object Value { get; set; }
    
            public object Simbol { get; set; }
    
            public object Description { get; set; }
        }
    }
}
