namespace v4posme_library.ModelsDto;

public record TbItemWarehouseDto()
{
    public string? Codigo { get; set; }
    public string? Producto { get; set; }
    public string? UM { get; set; }
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Cost { get; set; }
    public string? ItemNumber { get; set; }
    public string? ItemName { get; set; }
    public decimal QuantityMin { get; set; }
    public string? Number { get; set; }
    public string? WarehouseName { get; set; }
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int WarehouseId { get; set; }
    public decimal QuantityMax { get; set; }
}