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
    public partial class PrCxcGetReportCustomerCreditResult {

        public PrCxcGetReportCustomerCreditResult()
        {
            OnCreated();
        }

        public string? CustomerNumber { get; set; }

        public string? LegalName { get; set; }

        public string? CommercialName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public decimal? LimitCredit { get; set; }

        public decimal? BalanceCredit { get; set; }

        public decimal? TipoCambioCompra { get; set; }

        public decimal? TipoCambioVenta { get; set; }

        public string? Factura { get; set; }

        public string? Moneda { get; set; }

        public decimal? CapitalPrestado { get; set; }

        public int? MaxDiasMora { get; set; }

        public decimal? MontoAtrazado { get; set; }

        public decimal? CapitalAtrazado { get; set; }

        public decimal? InteresAtrazado { get; set; }

        public decimal? CapitalPagado { get; set; }

        public decimal? InteresPagado { get; set; }

        public DateTime? ProximoPago { get; set; }

        public decimal? MontoProximoPago { get; set; }

        public DateTime? UltimoPagoFecha { get; set; }

        public string? Direccion { get; set; }

        public string? Identification { get; set; }

        public string? Phone { get; set; }

        public string? LastShareNumber { get; set; }

        public DateTime? DateLastShareNumber { get; set; }

        public decimal? AmountLastShareNumber { get; set; }

        public string? LastVisit { get; set; }

        public decimal? RemainingDocument { get; set; }

        public string? EmployerName { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
