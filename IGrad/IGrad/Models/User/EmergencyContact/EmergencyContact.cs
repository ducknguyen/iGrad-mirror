﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class EmergencyContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public Name Name { get; set; }
        public string Relationship { get; set; }
        public ICollection<Phone> PhoneNumbers { get; set; } 
    }
}