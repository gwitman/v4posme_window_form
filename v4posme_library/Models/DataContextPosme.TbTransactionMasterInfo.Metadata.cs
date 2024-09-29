using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbTransactionMasterInfo.Metadata))]
    public partial class TbTransactionMasterInfo
    {
        public partial class Metadata
        {
    
            [Key]
            public object TransactionMasterInfoID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object TransactionID { get; set; }
    
            public object TransactionMasterID { get; set; }
    
            public object ZoneID { get; set; }
    
            public object RouteID { get; set; }
    
            public object MesaID { get; set; }
    
            public object ReferenceClientName { get; set; }
    
            public object ReferenceClientIdentifier { get; set; }
    
            public object ChangeAmount { get; set; }
    
            public object ReceiptAmountPoint { get; set; }
    
            public object ReceiptAmount { get; set; }
    
            public object ReceiptAmountDol { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object ReceiptAmountBank { get; set; }
    
            public object ReceiptAmountBankID { get; set; }
    
            public object ReceiptAmountBankReference { get; set; }
    
            public object ReceiptAmountBankDol { get; set; }
    
            public object ReceiptAmountBankDolID { get; set; }
    
            public object ReceiptAmountBankDolReference { get; set; }
    
            public object ReceiptAmountCard { get; set; }
    
            public object ReceiptAmountCardBankID { get; set; }
    
            public object ReceiptAmountCardBankReference { get; set; }
    
            public object ReceiptAmountCardDol { get; set; }
    
            public object ReceiptAmountCardBankDolID { get; set; }
    
            public object ReceiptAmountCardBankDolReference { get; set; }
        }
    }
}
