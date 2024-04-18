namespace v4posme_library.ModelsDto;

public record TbCustomerDto()
{
    public string? CustomerNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public decimal? BalancePoint { get; set; }
    public DateOnly? DateContract { get; set; }
    public int? EntityContactId { get; set; }
    public string? Reference3 { get; set; }
    public string? Reference4 { get; set; }
    public string? Reference5 { get; set; }
    public string? Reference6 { get; set; }
    public decimal? Budget { get; set; }
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int EntityId { get; set; }
    public int? IdentificationType { get; set; }
    public string? Identification { get; set; }
    public int? CountryId { get; set; }
    public int? StateId { get; set; }
    public int? CityId { get; set; }
    public string? Location { get; set; }
    public string? Address { get; set; }
    public int? CurrencyId { get; set; }
    public int? ClasificationId { get; set; }
    public int? CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public int? CustomerTypeId { get; set; }
    public int? StatusId { get; set; }
    public int? TypePay { get; set; }
    public int? PayConditionId { get; set; }
    public int? SexoId { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public string? CreatedIn { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public int? CreatedAt { get; set; }
    public bool? IsActive { get; set; }
    public int? TypeFirm { get; set; }
    public byte[] PhoneNumber { get; set; }
    public int CustomerId { get; set; }
}