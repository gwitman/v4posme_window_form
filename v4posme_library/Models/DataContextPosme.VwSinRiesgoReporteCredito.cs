﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 26/9/2024 4:44:37 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

#nullable enable annotations
#nullable disable warnings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace v4posme_library.Models
{
    /// <summary>
    /// VIEW
    /// </summary>
    public partial class VwSinRiesgoReporteCredito {

        public VwSinRiesgoReporteCredito()
        {
            OnCreated();
        }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int CustomerCreditDocumentID { get; set; }

        [Required()]
        public int EntityID { get; set; }

        [StringLength(2)]
        [Required()]
        public string TIPODEENTIDAD { get; set; }

        [StringLength(3)]
        [Required()]
        public string NUMEROCORRELATIVO { get; set; }

        [StringLength(10)]
        public string? FECHADEREPORTE { get; set; }

        [StringLength(2)]
        [Required()]
        public string DEPARTAMENTO { get; set; }

        [StringLength(250)]
        [Required()]
        public string NUMERODECEDULAORUC { get; set; }

        [StringLength(501)]
        public string? NOMBREDEPERSONA { get; set; }

        [StringLength(2)]
        public string? TIPODECREDITO { get; set; }

        [StringLength(10)]
        public string? FECHADEDESEMBOLSO { get; set; }

        [StringLength(2)]
        public string? TIPODEOBLIGACION { get; set; }

        public decimal? MONTOAUTORIZADO { get; set; }

        public decimal? PLAZO { get; set; }

        [StringLength(2)]
        [Required()]
        public string FRECUENCIADEPAGO { get; set; }

        public decimal? SALDODEUDA { get; set; }

        [StringLength(3)]
        public string? ESTADO { get; set; }

        public decimal? MONTOVENCIDO { get; set; }

        public int? ANTIGUEDADDEMORA { get; set; }

        [StringLength(2)]
        public string? TIPODEGARANTIA { get; set; }

        [StringLength(2)]
        public string? FORMADERECUPERACION { get; set; }

        [StringLength(50)]
        [Required()]
        public string NUMERODECREDITO { get; set; }

        public decimal? VALORDELACUOTA { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}