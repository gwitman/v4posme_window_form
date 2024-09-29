using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbNotification.Metadata))]
    public partial class TbNotification
    {
        public partial class Metadata
        {
    
            [Key]
            public object NotificationID { get; set; }
    
            public object ErrorID { get; set; }
    
            public object From { get; set; }
    
            public object To { get; set; }
    
            public object Subject { get; set; }
    
            public object Message { get; set; }
    
            public object Summary { get; set; }
    
            public object Title { get; set; }
    
            public object TagID { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object IsActive { get; set; }
    
            public object PhoneFrom { get; set; }
    
            public object PhoneTo { get; set; }
    
            public object ProgramDate { get; set; }
    
            public object ProgramHour { get; set; }
    
            public object SendOn { get; set; }
    
            public object SendEmailOn { get; set; }
    
            public object SendWhatsappOn { get; set; }
    
            public object AddedCalendarGoogle { get; set; }
    
            public object QuantityOcupation { get; set; }
    
            public object QuantityDisponible { get; set; }
    
            public object GoogleCalendarEventID { get; set; }
        }
    }
}
