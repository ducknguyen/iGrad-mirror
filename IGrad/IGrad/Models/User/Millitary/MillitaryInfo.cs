using System;
using System.Collections.Generic;
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

        public bool ArmedForcesActiveDuty { get; set; }
        public bool ArmedForcesReserved { get; set; }
        public bool NationalGuard { get; set; }
        public bool MoreThanOne { get; set; }
        public bool None { get; set; }
        public bool PreferNotToAnswer { get; set; }
    }
}