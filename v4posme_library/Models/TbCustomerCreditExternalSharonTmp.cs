using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
[Table("tb_customer_credit_external_sharon_tmp")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerCreditExternalSharonTmp
{
    [Column("companyName")]
    [StringLength(50)]
    public string CompanyName { get; set; } = null!;

    [Column("customerID")]
    public int CustomerId { get; set; }

    [Column("dateCredit")]
    [StringLength(50)]
    public string DateCredit { get; set; } = null!;

    [Column("documentNumber")]
    [StringLength(50)]
    public string DocumentNumber { get; set; } = null!;

    [Column("customerName")]
    [StringLength(250)]
    public string CustomerName { get; set; } = null!;

    [Column("customerIdentification")]
    [StringLength(250)]
    public string CustomerIdentification { get; set; } = null!;

    [Column("customerPhone")]
    [StringLength(250)]
    public string CustomerPhone { get; set; } = null!;

    [Column("amountAurotize")]
    [Precision(19, 2)]
    public decimal AmountAurotize { get; set; }

    [Column("plazo")]
    [StringLength(50)]
    public string Plazo { get; set; } = null!;

    [Column("formPay")]
    [StringLength(50)]
    public string FormPay { get; set; } = null!;

    [Column("amountShare")]
    [Precision(19, 2)]
    public decimal AmountShare { get; set; }

    [Column("amountBalance")]
    [Precision(19, 2)]
    public decimal AmountBalance { get; set; }

    [Column("dayMora")]
    [StringLength(50)]
    public string DayMora { get; set; } = null!;

    [Column("address")]
    [StringLength(450)]
    public string Address { get; set; } = null!;

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [Column("createdOn")]
    public DateOnly CreatedOn { get; set; }
}
