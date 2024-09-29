using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbLog.Metadata))]
    public partial class TbLog
    {
        public partial class Metadata
        {
    
            [Key]
            public object LogID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object LoginID { get; set; }
    
            public object Token { get; set; }
    
            public object ProcedureName { get; set; }
    
            public object Code { get; set; }
    
            public object Description { get; set; }
    
            public object CreatedOn { get; set; }
        }
    }
}
