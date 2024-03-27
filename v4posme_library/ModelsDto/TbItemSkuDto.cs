namespace v4posme_library.ModelsDto;

public record TbItemSkuDto()
{
    public int SkuId { get; set; }
    public int ItemId { get; set; }
    public int CatalogItemId { get; set; }
    public decimal Value { get; set; }
    public string? Sku { get; set; }
}