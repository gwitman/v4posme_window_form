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
    public partial class TbCustomerFrecuencyActuation {

        public TbCustomerFrecuencyActuation()
        {
            OnCreated();
        }

        public TbCustomerFrecuencyActuation(int customerFrecuencyActuations, int? entityID, DateTime? createdOn, string? name, int? situationID, int? frecuencyContactID, int? isActive, int? isApply) : this()        {
            this.CustomerFrecuencyActuations = customerFrecuencyActuations;
            this.EntityID = entityID;
            this.CreatedOn = createdOn;
            this.Name = name;
            this.SituationID = situationID;
            this.FrecuencyContactID = frecuencyContactID;
            this.IsActive = isActive;
            this.IsApply = isApply;
        }

        [Key]
        [Required()]
        public int CustomerFrecuencyActuations { get; set; }

        public int? EntityID { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(255)]
        public string? Name { get; set; }

        public int? SituationID { get; set; }

        public int? FrecuencyContactID { get; set; }

        public int? IsActive { get; set; }

        public int? IsApply { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}