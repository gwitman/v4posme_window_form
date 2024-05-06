using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_profile_detail_tmp")]
[Index("CompanyId", "BranchId", "LoginId", "TransactionId", Name = "IDX_1")]
[Index("CompanyId", "BranchId", "LoginId", "TransactionId", "TransactionMasterId", "TransactionCausalId", Name = "IDX_2")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionProfileDetailTmp
{
    [Column("companyID")]
    public int? CompanyId { get; set; }

    [Column("branchID")]
    public int? BranchId { get; set; }

    [Column("loginID")]
    public int? LoginId { get; set; }

    [Column("transactionID")]
    public int? TransactionId { get; set; }

    [Column("transactionMasterID")]
    public int? TransactionMasterId { get; set; }

    [Column("transactionCausalID")]
    public int? TransactionCausalId { get; set; }

    [Column("conceptID")]
    public int? ConceptId { get; set; }

    [Column("accountID")]
    public int? AccountId { get; set; }

    [Column("classID")]
    public int? ClassId { get; set; }

    [Column("debit")]
    [Precision(26, 8)]
    public decimal? Debit { get; set; }

    [Column("credit")]
    [Precision(26, 8)]
    public decimal? Credit { get; set; }

    [Key]
    [Column("transactionProfileDetailTmpID")]
    public int TransactionProfileDetailTmpId { get; set; }
}
