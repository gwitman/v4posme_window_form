using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master")]
[Index("CompanyId", Name = "IDX_TRANSACTION_MASTER_001")]
[Index("TransactionNumber", Name = "IDX_TRANSACTION_MASTER_002")]
[Index("TransactionId", Name = "IDX_TRANSACTION_MASTER_003")]
[Index("BranchId", Name = "IDX_TRANSACTION_MASTER_004")]
[Index("TransactionCausalId", Name = "IDX_TRANSACTION_MASTER_005")]
[Index("EntityId", Name = "IDX_TRANSACTION_MASTER_006")]
[Index("ComponentId", Name = "IDX_TRANSACTION_MASTER_007")]
[Index("CurrencyId", Name = "IDX_TRANSACTION_MASTER_008")]
[Index("CurrencyId2", Name = "IDX_TRANSACTION_MASTER_009")]
[Index("StatusId", Name = "IDX_TRANSACTION_MASTER_010")]
[Index("JournalEntryId", Name = "IDX_TRANSACTION_MASTER_011")]
[Index("ClassId", Name = "IDX_TRANSACTION_MASTER_012")]
[Index("AreaId", Name = "IDX_TRANSACTION_MASTER_013")]
[Index("PriorityId", Name = "IDX_TRANSACTION_MASTER_014")]
[Index("SourceWarehouseId", Name = "IDX_TRANSACTION_MASTER_015")]
[Index("TargetWarehouseId", Name = "IDX_TRANSACTION_MASTER_016")]
[Index("PeriodPay", Name = "IDX_TRANSACTION_MASTER_017")]
[Index("NotificationId", Name = "IDX_TRANSACTION_MASTER_018")]
[Index("EntityIdsecondary", Name = "IDX_TRANSACTION_MASTER_019")]
[Index("TransactionMasterId", Name = "IDX_TRANSACTOIN_MASTER_020")]
[Index("CompanyId", "TransactionNumber", Name = "IDX_TRANSACTOIN_MASTER_021")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionMaster
{
    [Key]
    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transactionNumber")]
    [StringLength(250)]
    public string TransactionNumber { get; set; } = null!;

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Column("branchID")]
    public int? BranchId { get; set; }

    [Column("transactionCausalID")]
    public int? TransactionCausalId { get; set; }

    [Column("entityID")]
    public int? EntityId { get; set; }

    [Column("transactionOn", TypeName = "datetime")]
    public DateTime? TransactionOn { get; set; }

    [Column("transactionOn2", TypeName = "datetime")]
    public DateTime? TransactionOn2 { get; set; }

    [Column("statusIDChangeOn", TypeName = "datetime")]
    public DateTime? StatusIdchangeOn { get; set; }

    [Column("componentID")]
    public int? ComponentId { get; set; }

    [Column("note")]
    [StringLength(3500)]
    public string? Note { get; set; }

    [Column("sign")]
    public short? Sign { get; set; }

    [Column("currencyID")]
    public int? CurrencyId { get; set; }

    [Column("currencyID2")]
    public int? CurrencyId2 { get; set; }

    [Column("exchangeRate")]
    [Precision(18, 4)]
    public decimal? ExchangeRate { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(250)]
    public string? Reference3 { get; set; }

    [Column("reference4")]
    [StringLength(250)]
    public string? Reference4 { get; set; }

    [Column("descriptionReference")]
    [StringLength(4000)]
    public string DescriptionReference { get; set; } = null!;

    [Column("statusID")]
    public int? StatusId { get; set; }

    [Column("amount")]
    [Precision(18, 4)]
    public decimal? Amount { get; set; }

    [Column("tax1")]
    [Precision(18, 4)]
    public decimal? Tax1 { get; set; }

    [Column("tax2")]
    [Precision(18, 4)]
    public decimal? Tax2 { get; set; }

    [Column("tax3")]
    [Precision(18, 4)]
    public decimal? Tax3 { get; set; }

    [Column("tax4")]
    [Precision(18, 4)]
    public decimal? Tax4 { get; set; }

    [Column("discount")]
    [Precision(18, 4)]
    public decimal? Discount { get; set; }

    [Column("subAmount")]
    [Precision(18, 4)]
    public decimal? SubAmount { get; set; }

    [Column("isApplied")]
    public bool? IsApplied { get; set; }

    [Column("journalEntryID")]
    public int? JournalEntryId { get; set; }

    [Column("classID")]
    public int? ClassId { get; set; }

    [Column("areaID")]
    public int? AreaId { get; set; }

    [Column("priorityID")]
    public int? PriorityId { get; set; }

    [Column("sourceWarehouseID")]
    public int? SourceWarehouseId { get; set; }

    [Column("targetWarehouseID")]
    public int? TargetWarehouseId { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }

    [Column("createdAt")]
    public int? CreatedAt { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("isTemplate")]
    public int? IsTemplate { get; set; }

    [Column("periodPay")]
    public int? PeriodPay { get; set; }

    [Column("nextVisit", TypeName = "datetime")]
    public DateTime? NextVisit { get; set; }

    [Column("numberPhone")]
    [StringLength(250)]
    public string? NumberPhone { get; set; }

    [Column("notificationID")]
    public int? NotificationId { get; set; }

    [Column("printerQuantity")]
    public int? PrinterQuantity { get; set; }

    [Column("entityIDSecondary")]
    public int? EntityIdsecondary { get; set; }
}
