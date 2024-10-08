using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMaster.Metadata))]
    public partial class TbTransactionMaster
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionMasterID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionNumber { get; set; }
    
            public object TransactionID { get; set; }
    
            public object BranchID { get; set; }
    
            public object TransactionCausalID { get; set; }
    
            public object EntityID { get; set; }
    
            public object TransactionOn { get; set; }
    
            public object TransactionOn2 { get; set; }
    
            public object StatusIDChangeOn { get; set; }
    
            public object ComponentID { get; set; }
    
            public object Note { get; set; }
    
            public object Sign { get; set; }
    
            public object CurrencyID { get; set; }
    
            public object CurrencyID2 { get; set; }
    
            public object ExchangeRate { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object Reference4 { get; set; }
    
            public object DescriptionReference { get; set; }
    
            public object StatusID { get; set; }
    
            public object Amount { get; set; }
    
            public object Tax1 { get; set; }
    
            public object Tax2 { get; set; }
    
            public object Tax3 { get; set; }
    
            public object Tax4 { get; set; }
    
            public object Discount { get; set; }
    
            public object SubAmount { get; set; }
    
            public object IsApplied { get; set; }
    
            public object JournalEntryID { get; set; }
    
            public object ClassID { get; set; }
    
            public object AreaID { get; set; }
    
            public object PriorityID { get; set; }
    
            public object SourceWarehouseID { get; set; }
    
            public object TargetWarehouseID { get; set; }
    
            public object CreatedBy { get; set; }
    
            public object CreatedAt { get; set; }
    
            public object CreatedOn { get; set; }
    
            public object CreatedIn { get; set; }
    
            public object IsActive { get; set; }
    
            public object IsTemplate { get; set; }
    
            public object PeriodPay { get; set; }
    
            public object NextVisit { get; set; }
    
            public object NumberPhone { get; set; }
    
            public object NotificationID { get; set; }
    
            public object PrinterQuantity { get; set; }
    
            public object EntityIDSecondary { get; set; }
    
            public object DayExcluded { get; set; }
        }
    }
}
