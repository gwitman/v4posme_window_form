namespace v4posme_library.ModelsDto;

public record TbTransactionMasterInfoDto()
{
    public int TransactionMasterInfoId { get; set; }
    public int CompanyId { get; set; }
    public int TransactionId { get; set; }
    public int TransactionMasterId { get; set; }
    public int? ZoneId { get; set; }
    public int? RouteId { get; set; }
    public int MesaId { get; set; }
    public string? ReferenceClientName { get; set; }
    public string? ReferenceClientIdentifier { get; set; }
    public decimal ChangeAmount { get; set; }
    public decimal? ReceiptAmountPoint { get; set; }
    public decimal? ReceiptAmount { get; set; }
    public decimal ReceiptAmountDol { get; set; }
    public decimal ReceiptAmountBank { get; set; }
    public int? ReceiptAmountBankId { get; set; }
    public string? ReceiptAmountBankReference { get; set; }
    public decimal ReceiptAmountBankDol { get; set; }
    public int? ReceiptAmountBankDolId { get; set; }
    public string? ReceiptAmountBankDolReference { get; set; }
    public decimal ReceiptAmountCard { get; set; }
    public int? ReceiptAmountCardBankId { get; set; }
    public string? ReceiptAmountCardBankReference { get; set; }
    public decimal ReceiptAmountCardDol { get; set; }
    public int? ReceiptAmountCardBankDolId { get; set; }
    public string? ReceiptAmountCardBankDolReference { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public string? ZonaName { get; set; }
    public string? MesaName { get; set; }
}