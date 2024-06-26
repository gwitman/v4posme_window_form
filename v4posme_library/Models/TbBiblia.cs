﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_biblia")]
[Index("Dia", Name = "IDX_BIBLIA_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbBiblia
{
    [Key]
    [Column("versiculoID")]
    public int VersiculoId { get; set; }

    [Column("orden")]
    public int Orden { get; set; }

    [Column("dia")]
    public int Dia { get; set; }

    [Column("capitulo")]
    public int Capitulo { get; set; }

    [Column("libro")]
    [StringLength(255)]
    public string Libro { get; set; } = null!;

    [Column("versiculo")]
    [StringLength(1500)]
    public string? Versiculo { get; set; }
}
