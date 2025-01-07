using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEmployeeCalendarPayDetail.Metadata))]
    public partial class TbEmployeeCalendarPayDetail
    {
        public partial class Metadata
        {
    
            [Key]
            public object CalendarDetailID { get; set; }
    
            public object CalendarID { get; set; }
    
            public object EmployeeID { get; set; }
    
            public object PlusSalary { get; set; }
    
            public object PlusCommission { get; set; }
    
            public object PlusBonus { get; set; }
    
            public object MinusAdelantos { get; set; }
    
            public object MinusDeductionForLoans { get; set; }
    
            public object MinusDeductionForLateArrival { get; set; }
    
            public object MinusInss { get; set; }
    
            public object InssPatronal { get; set; }
    
            public object MinusIr { get; set; }
    
            public object Saving { get; set; }
    
            public object EqualNeto { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
