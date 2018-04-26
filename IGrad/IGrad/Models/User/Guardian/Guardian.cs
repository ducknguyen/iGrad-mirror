using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class Guardian
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public Name Name { get; set; }
        [DisplayName("Phone One")]
        public Phone PhoneOne { get; set; }
        [DisplayName("Phone Two")]
        public Phone PhoneTwo { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        [DisplayName("Guardian Is Active Military?")]
        public bool IsActiveMilitary { get; set; }
        [DisplayName("Guardian's Residence Type")]
        public string GuardianResidenceType { get; set; }

        public enum EGuardianType
        {
            Primary,
            Secondary
        }

    }
}