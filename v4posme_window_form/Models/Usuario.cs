using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v4posme_window_form.Models
{
    [Table("tb_user")]
    public class Usuario
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        [Key]
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }
        public int EmployeeID { get; set; }
        public int UseMobile { get; set; }
        public string Phone { get; set; }
        public DateTime? LastPayment { get; set; }
        public string Comercio { get; set; }
        public string Foto { get; set; }

        [Column("token_google_calendar")]
        public string TokenGoogleCalendar { get; set; }
    }
}
