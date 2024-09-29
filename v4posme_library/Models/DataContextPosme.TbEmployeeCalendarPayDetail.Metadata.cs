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
    
            public object Salary { get; set; }
    
            public object Commission { get; set; }
    
            public object Adelantos { get; set; }
    
            public object Neto { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
