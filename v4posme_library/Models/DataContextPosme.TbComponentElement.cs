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
    public partial class TbComponentElement {

        public TbComponentElement()
        {
            this.ElementID = 0;
            this.ComponentID = 0;
            OnCreated();
        }

        public TbComponentElement(int componentElementID) : this()        {
            this.ComponentElementID = componentElementID;
        }

        [Key]
        [Required()]
        public int ComponentElementID { get; set; }

        [Required()]
        public int ElementID { get; set; }

        [Required()]
        public int ComponentID { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}