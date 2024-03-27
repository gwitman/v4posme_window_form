namespace v4posme_library.ModelsDto;

public record TbItemDto()
{
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int InventoryCategoryId { get; set; }
    public int ItemId { get; set; }
    public int? FamilyId { get; set; }
    public string? ItemNumber { get; set; }
    public string? BarCode { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? UnitMeasureId { get; set; }
    public int? DisplayId { get; set; }
    public int? Capacity { get; set; }
    public int? DisplayUnitMeasureId { get; set; }
    public int? DefaultWarehouseId { get; set; }
    public decimal Quantity { get; set; }
    public decimal QuantityMax { get; set; }
    public decimal QuantityMin { get; set; }
    public decimal Cost { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public int? StatusId { get; set; }
    public bool? IsPerishable { get; set; }
    public decimal? FactorBox { get; set; }
    public decimal? FactorProgram { get; set; }
    public string? CreatedIn { get; set; }
    public int? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? IsActive { get; set; }
    public sbyte? IsInvoiceQuantityZero { get; set; }
    public sbyte? IsServices { get; set; }
    public int CurrencyId { get; set; }
    public int IsInvoice { get; set; }
    public string? Reference3 { get; set; }
    public string? UnitMeasureName { get; set; }
    public string? ItemNameLog { get; set; }
}