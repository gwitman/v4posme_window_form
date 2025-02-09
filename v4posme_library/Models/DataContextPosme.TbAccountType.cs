﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 3/1/2025 3:48:59 PM
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
    public partial class TbAccountType {

        public TbAccountType()
        {
            this.CompanyID = 0;
            this.Name = @"0";
            this.Naturaleza = @"0";
            this.CreatedBy = 0;
            this.CreatedAt = @"0";
            this.CreatedIn = @"0";
            OnCreated();
        }

        public TbAccountType(int accountTypeID, string? description, DateTime createdOn, bool? isActive) : this()        {
            this.AccountTypeID = accountTypeID;
            this.Description = description;
            this.CreatedOn = createdOn;
            this.IsActive = isActive;
        }

        [Key]
        [Required()]
        public int AccountTypeID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [StringLength(350)]
        [Required()]
        public string Name { get; set; }

        [StringLength(1)]
        [Required()]
        public string Naturaleza { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required()]
        public int CreatedBy { get; set; }

        [StringLength(250)]
        [Required()]
        public string CreatedAt { get; set; }

        [Required()]
        public DateTime CreatedOn { get; set; }

        [StringLength(250)]
        [Required()]
        public string CreatedIn { get; set; }

        public bool? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
