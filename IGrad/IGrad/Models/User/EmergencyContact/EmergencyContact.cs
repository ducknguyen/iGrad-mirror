using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IGrad.Models.User
{
    public class EmergencyContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public Name Name { get; set; }
        public string Relationship { get; set; }
        public IEnumerable<SelectListItem> RelationshipTypeSelectList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Mother", Value = "Mother" });
                items.Add(new SelectListItem { Text = "Father", Value = "Father" });
                items.Add(new SelectListItem { Text = "Grandparent", Value = "Grandparent" });
                items.Add(new SelectListItem { Text = "Step Mother", Value = "Step Mother" });
                items.Add(new SelectListItem { Text = "Step Father", Value = "Step Father" });
                items.Add(new SelectListItem { Text = "Guardian", Value = "Guardian" });
                items.Add(new SelectListItem { Text = "Other", Value = "Other" });
                return items;
            }
        }
        [DisplayName("Email Address")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        public string EmailAddress { get; set; }
        public Phone PhoneNumber { get; set; }
      
    }
}