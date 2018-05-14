using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web.Mvc;

namespace IGrad.Models.User
{
    public class MilitaryInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public string AffiliationType { get; set; }

        public IEnumerable<SelectListItem> MilitarySelectList
        {
            get
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "No Affiliation", Value = EMilitaryAffiliation.None.ToString() });
                items.Add(new SelectListItem { Text = "Prefer Not To Answer", Value = EMilitaryAffiliation.PreferNotToAnswer.ToString() });
                items.Add(new SelectListItem { Text = "U.S. Armed Forces Active Duty", Value = EMilitaryAffiliation.ArmedForcesActiveDuty.ToString() });
                items.Add(new SelectListItem { Text = "U.S. Armed Forces Reserves", Value = EMilitaryAffiliation.ArmedForcesReserved.ToString() });
                items.Add(new SelectListItem { Text = "National Guard Member", Value = EMilitaryAffiliation.NationalGuard.ToString() });
                items.Add(new SelectListItem { Text = "More Than One Member of Armed Forces/National Guard", Value = EMilitaryAffiliation.MoreThanOne.ToString() });
               
                return items;
            }
        }

        public enum EMilitaryAffiliation
        {
            ArmedForcesActiveDuty,
            ArmedForcesReserved,
            NationalGuard,
            MoreThanOne,
            None,
            PreferNotToAnswer

        }
    }
}