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
    public partial class VwSinRiesgoReporteCliente {

        public VwSinRiesgoReporteCliente()
        {
            OnCreated();
        }

        [StringLength(10)]
        public string? FECHAREPORTE { get; set; }

        [StringLength(250)]
        [Required()]
        public string IDENTIFICACION { get; set; }

        [StringLength(1)]
        [Required()]
        public string TIPODEPERSONA { get; set; }

        [StringLength(12)]
        [Required()]
        public string NACIONALIDAD { get; set; }

        [StringLength(1)]
        public string? SEXO { get; set; }

        [StringLength(10)]
        public string? FECHADENACIMIENTO { get; set; }

        [StringLength(3)]
        [Required()]
        public string ESTADOCIVIL { get; set; }

        [StringLength(250)]
        public string? DIRECCION { get; set; }

        [StringLength(2)]
        [Required()]
        public string DEPARTAMENTO { get; set; }

        [StringLength(2)]
        [Required()]
        public string MUNICIPIO { get; set; }

        [StringLength(250)]
        public string? DIRECCIONDETRABAJO { get; set; }

        [StringLength(2)]
        [Required()]
        public string DEPARTAMENTODETRABAJO { get; set; }

        [StringLength(2)]
        [Required()]
        public string MUNICIPIODETRABAJO { get; set; }

        [StringLength(250)]
        public string? TELEFONODOMICILIAR { get; set; }

        [StringLength(250)]
        public string? TELEFONOTRABAJO { get; set; }

        [StringLength(250)]
        public string? CELULAR { get; set; }

        [Required()]
        public string CORREOELECTRONICO { get; set; }

        [StringLength(11)]
        [Required()]
        public string OCUPACION { get; set; }

        [StringLength(8)]
        [Required()]
        public string ACTIVIDADECONOMICA { get; set; }

        [StringLength(7)]
        [Required()]
        public string SECTOR { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}