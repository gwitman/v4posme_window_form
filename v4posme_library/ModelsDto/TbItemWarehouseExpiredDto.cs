namespace v4posme_library.ModelsDto;

public record TbItemWarehouseExpiredDto()
{
    public string? ItemNumber { get; set; }
    public string? ItemName { get; set; }
    public int WarehouseNumber { get; set; }
    public string? WarehouseName { get; set; }
    public decimal? Quantity { get; set; }
    public string? Lote { get; set; }
    public DateTime DateExpired { get; set; }
}