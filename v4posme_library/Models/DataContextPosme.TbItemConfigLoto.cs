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
    public partial class TbItemConfigLoto {

        public TbItemConfigLoto()
        {
            this.IsActive = 1;
            this.MaxSale = 1.00m;
            this.Turno1Inicio = 0;
            this.Turno1Fin = 9;
            this.Turno2Inicio = 9;
            this.Turno2Fin = 14;
            this.Turno3Inicio = 14;
            this.Turno3Fin = 22;
            this.ItemID = 1;
            OnCreated();
        }

        public int ItemConfigLotoID { get; set; }

        public int IsActive { get; set; }

        public decimal MaxSale { get; set; }

        public int Turno1Inicio { get; set; }

        public int Turno1Fin { get; set; }

        public int Turno2Inicio { get; set; }

        public int Turno2Fin { get; set; }

        public int Turno3Inicio { get; set; }

        public int Turno3Fin { get; set; }

        public int ItemID { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
