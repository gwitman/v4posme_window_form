namespace v4posme_window.Libraries;

public class BillingEdit
{
    public bool CheckDetail { get; set; }
    public int TransactionMasterDetailId { get; set; }
    public int ItemNumber { get; set; }
    public string TransactionDetailName { get; set; }
    public string Sku { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Iva { get; set; }
    public decimal SkuQuantityBySku { get; set; }
    public decimal UnitaryPriceIndividual { get; set; }
    public string SkuFormatoDescription { get; set; }
    public decimal ItemPrecio2 { get; set; }
    public decimal ItemPrecio3 { get; set; }
    public string DetailVencimiento { get; set; }
}