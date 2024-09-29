using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbIndicatorHistory.Metadata))]
    public partial class TbIndicatorHistory
    {
        public partial class Metadata
        {
    
            [Key]
            public object IndicatorHistoryID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object IndicatorID { get; set; }
    
            public object DateOn { get; set; }
    
            public object Value { get; set; }
        }
    }
}
