using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbIndicatorTmp.Metadata))]
    public partial class TbIndicatorTmp
    {
        public partial class Metadata
        {
    
            [Key]
            public object IndicatorTmpID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object LoginID { get; set; }
    
            public object TokenID { get; set; }
    
            public object IndicadorID { get; set; }
    
            public object Value { get; set; }
        }
    }
}
