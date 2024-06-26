﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_customer_credit_document_entity_related")]
[Index("CustomerCreditDocumentId", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_ENTITY_RELATED_001")]
[Index("EntityId", Name = "IDX_CUSTOMER_CREDIT_DOCUMENT_ENTITY_RELATED_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerCreditDocumentEntityRelated
{
    [Key]
    [Column("ccEntityRelatedID")]
    public int CcEntityRelatedId { get; set; }

    [Column("customerCreditDocumentID")]
    public int CustomerCreditDocumentId { get; set; }

    [Column("entityID")]
    public int EntityId { get; set; }

    /// <summary>
    /// Permite saber el tipo de obligacion, DEUDOR O FIADOR
    /// </summary>
    [Column("type")]
    public int Type { get; set; }

    /// <summary>
    /// Tipo de Credito, Consumo, Vivienda
    /// </summary>
    [Column("typeCredit")]
    public int TypeCredit { get; set; }

    /// <summary>
    /// Estado del Credito , Saneado, Vigente, etc
    /// </summary>
    [Column("statusCredit")]
    public int StatusCredit { get; set; }

    /// <summary>
    /// Aval, Fiduciario, Pagare, etc
    /// </summary>
    [Column("typeGarantia")]
    public int TypeGarantia { get; set; }

    /// <summary>
    /// Forma de Recuperacion Recuperacion Normal, Arreglo de pago, Cobro Extra judicial
    /// </summary>
    [Column("typeRecuperation")]
    public int TypeRecuperation { get; set; }

    /// <summary>
    /// Para reportar a la sin riesgo se multiplica este valor por el desembolso
    /// </summary>
    [Column("ratioDesembolso")]
    [Precision(10, 8)]
    public decimal RatioDesembolso { get; set; }

    /// <summary>
    /// Para reportar a la sin riresgo se multiplica este valor po rel Saldo
    /// </summary>
    [Column("ratioBalance")]
    [Precision(10, 8)]
    public decimal RatioBalance { get; set; }

    /// <summary>
    /// Para reportar a la sin riesgo saldo vencido
    /// </summary>
    [Column("ratioBalanceExpired")]
    [Precision(10, 8)]
    public decimal RatioBalanceExpired { get; set; }

    /// <summary>
    /// Para reportar a la sin riego se multiplica este valor por la cuota
    /// </summary>
    [Column("ratioShare")]
    [Precision(10, 8)]
    public decimal RatioShare { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("createdIn")]
    [StringLength(120)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdAt")]
    public int CreatedAt { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
