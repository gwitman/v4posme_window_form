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
    public partial class TbCompanySubelementObligatory {

        public TbCompanySubelementObligatory()
        {
            this.CompanyID = 0;
            this.ElementID = 0;
            this.SubElementID = 0;
            OnCreated();
        }

        public TbCompanySubelementObligatory(int companySubelementObligatoryID) : this()        {
            this.CompanySubelementObligatoryID = companySubelementObligatoryID;
        }

        [Key]
        [Required()]
        public int CompanySubelementObligatoryID { get; set; }

        [Required()]
        public int CompanyID { get; set; }

        [Required()]
        public int ElementID { get; set; }

        [Required()]
        public int SubElementID { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
