namespace v4posme_library.ModelsDto;

public class TbItemDataSheetDetailDto
{
    public int ItemDataSheetDetailId { get; set; }
    public int ItemDataSheetId { get; set; }
    public int ItemId { get; set; }
    public decimal Quantity { get; set; }
    public int RelatedItemId { get; set; }
    public sbyte IsActive { get; set; }
    public string? ItemNumber { get; set; }
    public string? Name { get; set; }
}