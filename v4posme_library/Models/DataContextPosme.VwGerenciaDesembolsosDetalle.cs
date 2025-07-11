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
    public partial class VwGerenciaDesembolsosDetalle {

        public VwGerenciaDesembolsosDetalle()
        {
            OnCreated();
        }

        public string? Colaborador { get; set; }

        public string? NombreColaborador { get; set; }

        public string Cliente { get; set; }

        public string? NombreCliente { get; set; }

        public string Factura { get; set; }

        public int CreditAmortizationID { get; set; }

        public DateTime FechaCuota { get; set; }

        public string? AnoCuota { get; set; }

        public string? Mes1Cuota { get; set; }

        public string? Mes2Cuota { get; set; }

        public double? CBalanceStartCuota { get; set; }

        public double? CInteresCuota { get; set; }

        public double? CCapitalCuota { get; set; }

        public double? CBalanceEndCuota { get; set; }

        public double? CShareCuota { get; set; }

        public double? CRemainingCuota { get; set; }

        public double? CshareCapital { get; set; }

        public string? EstadoCuota { get; set; }

        public int? DiasAtrazoCuota { get; set; }

        public string Moneda { get; set; }

        public double? TipoCambioActual { get; set; }

        public double? CCapitalPagado { get; set; }

        public double? CCapitalPendiente { get; set; }

        public double? CIntaresPagado { get; set; }

        public double? CInteresPendiente { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
