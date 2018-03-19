using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IGrad.Models.Income
{
    public class IncomeTable
    {
        [Key]
        public int id { get; set; }

        public string Monthly { get; set; }
        public string TwiceMonthly { get; set; }
        public string TwoWeeks { get; set; }
        public string Weekly { get; set; }
        public string Annually { get; set; }
    }
}