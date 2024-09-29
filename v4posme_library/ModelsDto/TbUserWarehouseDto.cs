namespace v4posme_library.ModelsDto;

public record TbUserWarehouseDto()
{
    public int CompanyId { get; set; }
    public int WarehouseId { get; set; }
    public int BranchId { get; set; }
    public int UserId { get; set; }
    public string? Number { get; set; }
    public string? Name { get; set; }
    public int StatusId { get; set; }
    public bool IsActive { get; set; }
    public int TypeWarehouse { get; set; }
}