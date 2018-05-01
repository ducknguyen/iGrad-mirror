using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IGrad.Models.User
{
    public class MillitaryInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }

        [DisplayName("Armed Forces Active Duty")]
        public bool ArmedForcesActiveDuty { get; set; }
        [DisplayName("Armed Forces Reserved")]
        public bool ArmedForcesReserved { get; set; }
        [DisplayName("National Guard")]
        public bool NationalGuard { get; set; }
        [DisplayName("More Than one Military")]
        public bool MoreThanOne { get; set; }
        [DisplayName("None or Not Applicable")]
        public bool None { get; set; }
        [DisplayName("Prefer Not To Answer")]
        public bool PreferNotToAnswer { get; set; }
    }
}