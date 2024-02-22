using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwGerenciaCustomer
{
    [Column("customerNumber")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string CustomerNumber { get; set; } = null!;

    [Column("firstName")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? FirstName { get; set; }

    [Column("identification")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Identification { get; set; } = null!;

    [Column("birthDate")]
    public DateOnly? BirthDate { get; set; }
}
