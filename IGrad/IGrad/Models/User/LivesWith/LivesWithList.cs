using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace IGrad.Models.User
{
    public class LivesWith
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        [DisplayName("Total People In Residence")]
        public int TotalPeopleInResidence { get; set; }
        [DisplayName("Annual Household Income")]
        public int AnnualHouseHoldIncome { get; set; }
        [DisplayName("Name of Agency Providing Housing")]
        public string AgencyName { get; set; }
        public string Other { get; set; }
        public string StudentLivesWith { get; set; }
        public IEnumerable<SelectListItem> LivesWithSelectList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Both Parents", Value = ELiveWithOptions.LivesWithBothParents.ToString() });
                items.Add(new SelectListItem { Text = "Mother Only", Value = ELiveWithOptions.LivesWithMotherOnly.ToString() });
                items.Add(new SelectListItem { Text = "Father Only", Value = ELiveWithOptions.LivesWithFatherOnly.ToString() });
                items.Add(new SelectListItem { Text = "Grandparents", Value = ELiveWithOptions.LivesWithGrandparents.ToString() });
                items.Add(new SelectListItem { Text = "Provides Own Housing", Value = ELiveWithOptions.LivesWithSelf.ToString() });
                items.Add(new SelectListItem { Text = "Father and Step Mother", Value = ELiveWithOptions.LivesWithFatherAndStepMom.ToString() });
                items.Add(new SelectListItem { Text = "Mother and Step Father", Value = ELiveWithOptions.LivesWithMotherAndStepDad.ToString() });
                items.Add(new SelectListItem { Text = "Foster Parents", Value = ELiveWithOptions.LivesWithFosterParents.ToString() });
                items.Add(new SelectListItem { Text = "Agency Provides Housing", Value = ELiveWithOptions.LivesWithAgency.ToString() });
                items.Add(new SelectListItem { Text = "Homeless / Distressed", Value = ELiveWithOptions.HomelessDistressed.ToString() });
                items.Add(new SelectListItem { Text = "Other", Value = ELiveWithOptions.LivesWithOther.ToString() });
                return items;
            }
        }

        public enum ELiveWithOptions
        {
            LivesWithBothParents,
            LivesWithMotherOnly,
            LivesWithFatherOnly,
            LivesWithGrandparents,
            LivesWithSelf,
            LivesWithFatherAndStepMom,
            LivesWithMotherAndStepDad,
            LivesWithFosterParents,
            LivesWithAgency,
            HomelessDistressed,
            LivesWithOther
        }
    }
}