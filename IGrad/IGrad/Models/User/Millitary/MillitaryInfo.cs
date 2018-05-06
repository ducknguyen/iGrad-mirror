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

        [DisplayName("U.S. Armed Forces active duty")]
        public bool ArmedForcesActiveDuty { get; set; }
        [DisplayName("U.S. Armed Forces reserves")]
        public bool ArmedForcesReserved { get; set; }
        [DisplayName("National Guard member")]
        public bool NationalGuard { get; set; }
        [DisplayName("More than one member of Armed Forces/Nat")]
        public bool MoreThanOne { get; set; }
        [DisplayName("No Affiliation")]
        public bool None { get; set; }
        [DisplayName("No response/refused to state")]
        public bool PreferNotToAnswer { get; set; }
    }
}