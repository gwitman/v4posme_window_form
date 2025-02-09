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
    public partial class TbAccount {

        public TbAccount()
        {
            this.CompanyID = 0;
            this.AccountTypeID = 0;
            this.AccountLevelID = 0;
            this.AccountNumber = @"0";
            this.Name = @"0";
            this.IsOperative = false;
            this.StatusID = 0;
            this.CurrencyID = 0;
            OnCreated();
        }

        public TbAccount(int accountID, int? parentAccountID, int? classID, string? description, int? createdBy, DateTime? createdOn, string? createdIn, string? createdAt, bool? isActive) : this()        {
            this.AccountID = accountID;
            this.ParentAccountID = parentAccountID;
            this.ClassID = classID;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedOn = createdOn;
            this.CreatedIn = createdIn;
            this.CreatedAt = createdAt;
            this.IsActive = isActive;
        }

        [Key]
        [Required()]
        public int AccountID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int AccountTypeID { get; set; }

        [Required()]
        public int AccountLevelID { get; set; }

        public int? ParentAccountID { get; set; }

        public int? ClassID { get; set; }

        [StringLength(250)]
        [Required()]
        public string AccountNumber { get; set; }

        [StringLength(250)]
        [Required()]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required()]
        public bool IsOperative { get; set; }

        [Required()]
        public int StatusID { get; set; }

        [Required()]
        public int CurrencyID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(250)]
        public string? CreatedIn { get; set; }

        [StringLength(250)]
        public string? CreatedAt { get; set; }

        public bool? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
