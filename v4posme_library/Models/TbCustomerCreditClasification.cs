using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer_credit_clasification")]
[Index("EntityId", Name = "IDX_CUSTOMER_CREDIT_CLASIFICATION_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerCreditClasification
{
    [Key]
    [Column("clasificationID", TypeName = "int(11)")]
    public int ClasificationId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("dateHistory")]
    public DateOnly DateHistory { get; set; }

    [Column("numberShareLate", TypeName = "int(11)")]
    public int NumberShareLate { get; set; }

    [Column("amountCapitalLate")]
    [Precision(18, 8)]
    public decimal AmountCapitalLate { get; set; }

    [Column("amountInterestLate")]
    [Precision(18, 8)]
    public decimal AmountInterestLate { get; set; }

    [Column("maxDayMora", TypeName = "int(11)")]
    public int MaxDayMora { get; set; }

    [Column("numberCreditAbiertos", TypeName = "int(11)")]
    public int NumberCreditAbiertos { get; set; }

    [Column("numberCreditSaneados", TypeName = "int(11)")]
    public int NumberCreditSaneados { get; set; }

    [Column("numberCreditCancelados", TypeName = "int(11)")]
    public int NumberCreditCancelados { get; set; }

    [Column("amountCapitalAbierto")]
    [Precision(18, 8)]
    public decimal AmountCapitalAbierto { get; set; }

    [Column("amountCapitalSaneado")]
    [Precision(18, 8)]
    public decimal AmountCapitalSaneado { get; set; }

    [Column("amountCapitalCancelado")]
    [Precision(18, 8)]
    public decimal AmountCapitalCancelado { get; set; }

    [Column("summary", TypeName = "int(11)")]
    public int Summary { get; set; }
}
