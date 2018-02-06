﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class LivesWithList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public bool LivesWithBothParents { get; set; }
        public bool LivesWithMotherOnly { get; set; }
        public bool LivesWithFatherOnly { get; set; }
        public bool LivesWithGrandparents { get; set; }
        public bool LivesWithSelf { get; set; }
        public bool LivesWithFatherAndStepMom { get; set; }
        public bool LivesWithMotherAndStepDad { get; set; }
        public bool LivesWithFosterParents { get; set; }
        public bool LivesWithAgency { get; set; }
        public string AgencyName { get; set; }
        public string Other { get; set; }
    }
}