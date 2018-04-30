using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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

        public string Relationship { get; set; }
        public IEnumerable<SelectListItem> RelationshipTypeSelectList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Mother", Value = "Mother" });
                items.Add(new SelectListItem { Text = "Father", Value = "Father" });
                items.Add(new SelectListItem { Text = "Grandparent", Value = "Grandparent" });
                items.Add(new SelectListItem { Text = "Step Mother", Value = "StepMother" });
                items.Add(new SelectListItem { Text = "Step Father", Value = "StepFather" });
                items.Add(new SelectListItem { Text = "Guardian", Value = "Guardian" });
                items.Add(new SelectListItem { Text = "Other", Value = "Other" });
                return items;
            }
        }

    }
}