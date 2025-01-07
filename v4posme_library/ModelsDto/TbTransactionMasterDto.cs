using AutoMapper;
using v4posme_library.Models;

namespace v4posme_library.ModelsDto;

public record TbTransactionMasterDto()
{
    public int CompanyId { get; set; }
    public int TransactionId { get; set; }
    public int TransactionMasterId { get; set; }
    public int? BranchId { get; set; }
    public string? TransactionNumber { get; set; }
    public int? TransactionCausalId { get; set; }
    public int? EntityId { get; set; }
    public DateTime? TransactionOn { get; set; }
    public DateTime? StatusIdchangeOn { get; set; }
    public int? ComponentId { get; set; }
    public decimal? Tax1 { get; set; }
    public decimal? Tax2 { get; set; }
    public decimal? Tax3 { get; set; }
    public decimal? Tax4 { get; set; }
    public decimal? Discount { get; set; }
    public decimal? SubAmount { get; set; }
    public string? Note { get; set; }
    public short? Sign { get; set; }
    public int? CurrencyId { get; set; }
    public int? CurrencyId2 { get; set; }
    public decimal? ExchangeRate { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public string? Reference3 { get; set; }
    public string? Reference4 { get; set; }
    public int? StatusId { get; set; }
    public decimal? Amount { get; set; }
    public bool? IsApplied { get; set; }
    public int? JournalEntryId { get; set; }
    public int? ClassId { get; set; }
    public int? AreaId { get; set; }
    public int? SourceWarehouseId { get; set; }
    public int? TargetWarehouseId { get; set; }
    public int? CreatedBy { get; set; }
    public int? CreatedAt { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? CreatedIn { get; set; }
    public bool? IsActive { get; set; }
    public string? WorkflowStageName { get; set; }
    public int? PriorityId { get; set; }
    public DateTime? TransactionOn2 { get; set; }
    public int? IsTemplate { get; set; }
    public int? PeriodPay { get; set; }
    public DateTime? NextVisit { get; set; }
    public string? NumberPhone { get; set; }
    public int? PrinterQuantity { get; set; }
    public int? EntityIdsecondary { get; set; }
    public string? NameStatus { get; set; }

    public IList<TbTransactionMasterDetailDto> TransactionMasterDetail { get; set; }
    public int? DayExcluded { get; set; }
}