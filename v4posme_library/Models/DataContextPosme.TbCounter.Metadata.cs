using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbCounter.Metadata))]
    public partial class TbCounter
    {
        public partial class Metadata
        {
    
            [Key]
            public object CounterID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object BranchID { get; set; }
    
            public object ComponentItemID { get; set; }
    
            public object InitialValue { get; set; }
    
            public object CurrentValue { get; set; }
    
            public object Seed { get; set; }
    
            public object Serie { get; set; }
    
            public object Length { get; set; }
        }
    }
}
