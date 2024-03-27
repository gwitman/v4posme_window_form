namespace v4posme_library.ModelsDto;

public record TbTransactionCausalDto()
{
    public int CompanyId { get; set; }
    public int TransactionId { get; set; }
    public int TransactionCausalId { get; set; }
    public int? BranchId { get; set; }
    public string? Name { get; set; }
    public int? WarehouseSourceId { get; set; }
    public int? WarehouseTargetId { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public string? Branch { get; set; }
    public string? WarehouseSourceDescription { get; set; }
    public string? WarehouseTargetDescription { get; set; }
}