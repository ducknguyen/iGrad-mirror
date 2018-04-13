using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IGrad.Models.User
{
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        [DisplayName("Phone Number")]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")]
        public string PhoneNumber { get; set; }
        [DisplayName("Phone Type")]
        public string PhoneType { get; set; }
        public IEnumerable<SelectListItem> PhoneTypeSelectList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Mobile", Value = "Mobile", Selected = true });
                items.Add(new SelectListItem { Text = "Home", Value = "Home" });
                items.Add(new SelectListItem { Text = "Work", Value = "Work" });
                return items;
            }
        }
    }
}