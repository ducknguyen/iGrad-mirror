using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IGrad.Models.OpenHouse
{
    public class OpenHouse
    {
        [Key]
        public int id { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Open House Days:")]
        public string OpenDays { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("What you need to bring:")]
        public string WhatToBring { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Announcement(s):")]
        public string Announcements { get; set; }
    }
}