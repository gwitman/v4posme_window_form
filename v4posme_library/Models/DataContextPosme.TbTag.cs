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
    /// <summary>
    /// tabla para almacenar los tag de notificaciones
    /// </summary>
    public partial class TbTag {

        public TbTag()
        {
            OnCreated();
        }

        public TbTag(int tagID, string? name, string? description, bool? sendEmail, bool? sendNotificationApp, bool? sendSMS, bool? isActive) : this()        {
            this.TagID = tagID;
            this.Name = name;
            this.Description = description;
            this.SendEmail = sendEmail;
            this.SendNotificationApp = sendNotificationApp;
            this.SendSMS = sendSMS;
            this.IsActive = isActive;
        }

        [Key]
        [Required()]
        public int TagID { get; set; }

        [StringLength(150)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? SendEmail { get; set; }

        public bool? SendNotificationApp { get; set; }

        public bool? SendSMS { get; set; }

        public bool? IsActive { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
