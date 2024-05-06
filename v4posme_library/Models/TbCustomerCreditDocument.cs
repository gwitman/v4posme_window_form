using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer_credit_document")]
[Index("CompanyId", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_001")]
[Index("EntityId", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_002")]
[Index("CustomerCreditLineId", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_003")]
[Index("DocumentNumber", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_004")]
[Index("CurrencyId", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_005")]
[Index("TypeAmortization", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerCreditDocument
{
    [Key]
    [Column("customerCreditDocumentID")]
    public int CustomerCreditDocumentId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("entityID")]
    public int EntityId { get; set; }

    [Column("customerCreditLineID")]
    public int CustomerCreditLineId { get; set; }

    [Column("documentNumber")]
    [StringLength(50)]
    public string DocumentNumber { get; set; } = null!;

    [Column("dateOn")]
    public DateOnly DateOn { get; set; }

    [Column("amount")]
    [Precision(19, 9)]
    public decimal Amount { get; set; }

    [Column("interes")]
    [Precision(19, 9)]
    public decimal Interes { get; set; }

    [Column("term")]
    public int Term { get; set; }

    [Column("balance")]
    [Precision(19, 9)]
    public decimal Balance { get; set; }

    [Column("balanceProvicioned")]
    [Precision(19, 9)]
    public decimal BalanceProvicioned { get; set; }

    [Column("exchangeRate")]
    [Precision(18, 4)]
    public decimal ExchangeRate { get; set; }

    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("reference1")]
    [StringLength(4500)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(50)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(50)]
    public string? Reference3 { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [Column("typeAmortization")]
    public int TypeAmortization { get; set; }

    [Column("periodPay")]
    public int PeriodPay { get; set; }

    [Column("providerIDCredit")]
    public int ProviderIdcredit { get; set; }

    [Column("reportSinRiesgo")]
    public int ReportSinRiesgo { get; set; }
}
