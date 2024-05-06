using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_profile_detail")]
[Index("CompanyId", Name = "IDX_TRANSACTION_PROFILE_DETAIL_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_PROFILE_DETAIL_002")]
[Index("TransactionCausalId", Name = "IDX_TRANSACTION_PROFILE_DETAIL_003")]
[Index("ConceptId", Name = "IDX_TRANSACTION_PROFILE_DETAIL_004")]
[Index("AccountId", Name = "IDX_TRANSACTION_PROFILE_DETAIL_005")]
[Index("ClassId", Name = "IDX_TRANSACTION_PROFILE_DETAIL_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionProfileDetail
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Column("transactionCausalID")]
    public int TransactionCausalId { get; set; }

    [Key]
    [Column("profileDetailID")]
    public int ProfileDetailId { get; set; }

    [Column("conceptID")]
    public int ConceptId { get; set; }

    [Column("accountID")]
    [StringLength(50)]
    public string AccountId { get; set; } = null!;

    [Column("classID")]
    [StringLength(50)]
    public string? ClassId { get; set; }

    [Column("sign")]
    [StringLength(1)]
    public string Sign { get; set; } = null!;
}
