namespace v4posme_library.ModelsDto;

public record TbTransactionProfileDetailDto()
{
    public int CompanyId { get; set; }
    public int TransactionId { get; set; }
    public int TransactionCausalId { get; set; }
    public int ProfileDetailId { get; set; }
    public int ConceptId { get; set; }
    public string? AccountId { get; set; }
    public string? ClassId { get; set; }
    public string? Sign { get; set; }
    public string? ConceptDescription { get; set; }
    public string? AccountDescription { get; set; }
    public string? CenterCostDescription { get; set; }
}