using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbEmployeeCalendarPay.Metadata))]
    public partial class TbEmployeeCalendarPay
    {
        public partial class Metadata
        {
    
            [Key]
            public object CalendarID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object AccountingCycleID { get; set; }
    
            public object Name { get; set; }
    
            public object Number { get; set; }
    
            public object TypeID { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object StatusID { get; set; }
    
            public object Description { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
