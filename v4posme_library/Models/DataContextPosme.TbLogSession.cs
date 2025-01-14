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
    public partial class TbLogSession {

        public TbLogSession()
        {
            this.SessionId = @"0";
            this.IpAddress = @"0";
            this.UserAgent = @"0";
            this.LastActivity = @"0";
            OnCreated();
        }

        public TbLogSession(string? userData) : this()        {
            this.UserData = userData;
        }

        [Key]
        [StringLength(40)]
        [Required()]
        public string SessionId { get; set; }

        [StringLength(45)]
        [Required()]
        public string IpAddress { get; set; }

        [StringLength(120)]
        [Required()]
        public string UserAgent { get; set; }

        [StringLength(15)]
        [Required()]
        public string LastActivity { get; set; }

        public string? UserData { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
