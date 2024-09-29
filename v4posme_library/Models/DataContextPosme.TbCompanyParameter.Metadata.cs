using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyParameter.Metadata))]
    public partial class TbCompanyParameter
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyParameterID { get; set; }
    
            public object ParameterID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object Display { get; set; }
    
            public object Description { get; set; }
    
            public object Value { get; set; }
    
            public object CustomValue { get; set; }
        }
    }
}
