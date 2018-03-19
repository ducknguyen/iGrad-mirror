using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IGrad.Models.Income
{
    public class FamilyIncome
    {
        [Key]
        public int id { get; set; }

        public List<IncomeTable> incomeTable { get; set; }

        public string IncomeTableYears { get; set; }
        public string EffectiveDates { get; set; }

    }
}