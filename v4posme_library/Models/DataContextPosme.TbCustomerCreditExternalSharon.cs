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
    public partial class TbCustomerCreditExternalSharon {

        public TbCustomerCreditExternalSharon()
        {
            this.FECHADEREPORTE = @"0";
            this.DEPARTAMENTO = @"0";
            this.NUMERODECEDULAORUC = @"0";
            this.NOMBREDEPERSONA = @"0";
            this.TIPODECREDITO = @"0";
            this.FECHADEDESEMBOLSO = @"0";
            this.TIPODEOBLIGACION = @"0";
            this.MONTOAUTORIZADO = 0.00m;
            this.FRECUENCIADEPAGO = @"0";
            this.SALDODEUDA = 0.00m;
            this.ESTADO = @"0";
            this.MONTOVENCIDO = 0.00m;
            this.ANTIGUEDADDEMORA = @"0";
            this.TIPODEGARANTIA = @"0";
            this.FORMADERECUPERACION = @"0";
            this.NUMERODECREDITO = @"0";
            this.VALORDELACUOTA = 0.00m;
            this.IsActive = true;
            OnCreated();
        }

        public int CustomerID { get; set; }

        public int? CompanyID { get; set; }

        public string? TIPODEENTIDAD { get; set; }

        public string NUMEROCORRELATIVO { get; set; }

        public string FECHADEREPORTE { get; set; }

        public string DEPARTAMENTO { get; set; }

        public string NUMERODECEDULAORUC { get; set; }

        public string NOMBREDEPERSONA { get; set; }

        public string TIPODECREDITO { get; set; }

        public string FECHADEDESEMBOLSO { get; set; }

        public string TIPODEOBLIGACION { get; set; }

        public decimal MONTOAUTORIZADO { get; set; }

        public int? PLAZO { get; set; }

        public string FRECUENCIADEPAGO { get; set; }

        public decimal SALDODEUDA { get; set; }

        public string ESTADO { get; set; }

        public decimal MONTOVENCIDO { get; set; }

        public string ANTIGUEDADDEMORA { get; set; }

        public string TIPODEGARANTIA { get; set; }

        public string FORMADERECUPERACION { get; set; }

        public string NUMERODECREDITO { get; set; }

        public decimal VALORDELACUOTA { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
