using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCompanyDataview.Metadata))]
    public partial class TbCompanyDataview
    {
        public partial class Metadata
        {
    
            [Key]
            public object CompanyDataViewID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object DataViewID { get; set; }
    
            public object CallerID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object SqlScript { get; set; }
    
            public object VisibleColumns { get; set; }
    
            public object NonVisibleColumns { get; set; }
    
            public object SummaryColumns { get; set; }
    
            public object FormatColumns { get; set; }
    
            public object IsActive { get; set; }
    
            public object FlavorID { get; set; }
    
            public object FormatColumnsHeader { get; set; }
        }
    }
}
