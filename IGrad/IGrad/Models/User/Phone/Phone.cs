using System;
using System.Collections.Generic;
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
        public string PhoneNumber { get; set; }
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