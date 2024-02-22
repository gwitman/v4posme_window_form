using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer_credit_line")]
[Index("CompanyId", Name = "IDX_CUSTOMER_CREDIT_LINE_001")]
[Index("BranchId", Name = "IDX_CUSTOMER_CREDIT_LINE_002")]
[Index("EntityId", Name = "IDX_CUSTOMER_CREDIT_LINE_003")]
[Index("CreditLineId", Name = "IDX_CUSTOMER_CREDIT_LINE_004")]
[Index("AccountNumber", Name = "IDX_CUSTOMER_CREDIT_LINE_005")]
[Index("CurrencyId", Name = "IDX_CUSTOMER_CREDIT_LINE_006")]
[Index("TypeAmortization", Name = "IDX_CUSTOMER_CREDIT_LINE_007")]
[Index("StatusId", Name = "IDX_CUSTOMER_CREDIT_LINE_008")]
[Index("PeriodPay", Name = "IDX_CUSTOMER_CREDIT_LINE_009")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbCustomerCreditLine
{
    [Key]
    [Column("customerCreditLineID", TypeName = "int(11)")]
    public int CustomerCreditLineId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("creditLineID", TypeName = "int(11)")]
    public int CreditLineId { get; set; }

    [Column("accountNumber")]
    [StringLength(25)]
    public string AccountNumber { get; set; } = null!;

    [Column("currencyID", TypeName = "int(11)")]
    public int CurrencyId { get; set; }

    [Column("typeAmortization", TypeName = "int(11)")]
    public int TypeAmortization { get; set; }

    [Column("limitCredit")]
    [Precision(19, 9)]
    public decimal LimitCredit { get; set; }

    [Column("balance")]
    [Precision(19, 9)]
    public decimal Balance { get; set; }

    [Column("interestYear")]
    [Precision(19, 9)]
    public decimal InterestYear { get; set; }

    [Column("interestPay")]
    [Precision(19, 9)]
    public decimal InterestPay { get; set; }

    [Column("totalPay")]
    [Precision(19, 9)]
    public decimal TotalPay { get; set; }

    [Column("totalDefeated")]
    [Precision(19, 9)]
    public decimal TotalDefeated { get; set; }

    [Column("dateOpen")]
    public DateOnly DateOpen { get; set; }

    [Column("periodPay", TypeName = "int(11)")]
    public int PeriodPay { get; set; }

    [Column("dateLastPay")]
    public DateOnly? DateLastPay { get; set; }

    [Column("term", TypeName = "int(11)")]
    public int? Term { get; set; }

    [Column("note")]
    [StringLength(550)]
    public string? Note { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
