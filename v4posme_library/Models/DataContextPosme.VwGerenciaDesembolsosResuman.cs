﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2025-06-24 14:25:27
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable enable annotations
#nullable disable warnings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace v4posme_library.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public partial class VwGerenciaDesembolsosResuman {

        public VwGerenciaDesembolsosResuman()
        {
            OnCreated();
        }

        public string CodigoCliente { get; set; }

        public string? Nombre { get; set; }

        public string Moneda { get; set; }

        public int? Edad { get; set; }

        public decimal? CMonto { get; set; }

        public decimal? CBalance { get; set; }

        public decimal? CProvisionado { get; set; }

        public string? Estado { get; set; }

        public decimal Interes { get; set; }

        public int Plazo { get; set; }

        public decimal TipoCambio { get; set; }

        public DateTime Fecha { get; set; }

        public string? TipoAmortizacion { get; set; }

        public string? PeriodoPago { get; set; }

        public string? Anio { get; set; }

        public string? Mes { get; set; }

        public string? MesUnicamente { get; set; }

        public string Factura { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
