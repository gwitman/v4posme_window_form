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
    /// Tabla de Notificaciones
    /// </summary>
    public partial class TbNotification {

        public TbNotification()
        {
            this.QuantityOcupation = 0;
            this.QuantityDisponible = 0;
            OnCreated();
        }

        public TbNotification(int notificationID, int? errorID, string? from, string? to, string? subject, string? message, string? summary, string? title, int? tagID, DateTime? createdOn, bool? isActive, string? phoneFrom, string? phoneTo, string? programDate, string? programHour, DateTime? sendOn, DateTime? sendEmailOn, DateTime? sendWhatsappOn, bool? addedCalendarGoogle, string? googleCalendarEventID) : this()        {
            this.NotificationID = notificationID;
            this.ErrorID = errorID;
            this.From = from;
            this.To = to;
            this.Subject = subject;
            this.Message = message;
            this.Summary = summary;
            this.Title = title;
            this.TagID = tagID;
            this.CreatedOn = createdOn;
            this.IsActive = isActive;
            this.PhoneFrom = phoneFrom;
            this.PhoneTo = phoneTo;
            this.ProgramDate = programDate;
            this.ProgramHour = programHour;
            this.SendOn = sendOn;
            this.SendEmailOn = sendEmailOn;
            this.SendWhatsappOn = sendWhatsappOn;
            this.AddedCalendarGoogle = addedCalendarGoogle;
            this.GoogleCalendarEventID = googleCalendarEventID;
        }

        [Key]
        [Required()]
        public int NotificationID { get; set; }

        public int? ErrorID { get; set; }

        [StringLength(500)]
        public string? From { get; set; }

        [StringLength(500)]
        public string? To { get; set; }

        [StringLength(500)]
        public string? Subject { get; set; }

        [StringLength(5000)]
        public string? Message { get; set; }

        [StringLength(500)]
        public string? Summary { get; set; }

        [StringLength(500)]
        public string? Title { get; set; }

        public int? TagID { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(255)]
        public string? PhoneFrom { get; set; }

        [StringLength(255)]
        public string? PhoneTo { get; set; }

        [StringLength(255)]
        public string? ProgramDate { get; set; }

        [StringLength(255)]
        public string? ProgramHour { get; set; }

        public DateTime? SendOn { get; set; }

        public DateTime? SendEmailOn { get; set; }

        public DateTime? SendWhatsappOn { get; set; }

        public bool? AddedCalendarGoogle { get; set; }

        public int? QuantityOcupation { get; set; }

        public int? QuantityDisponible { get; set; }

        [StringLength(255)]
        public string? GoogleCalendarEventID { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
