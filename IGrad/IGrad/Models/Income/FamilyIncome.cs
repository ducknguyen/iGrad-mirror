using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IGrad.Models.Income
{
    public class FamilyIncome
    {
        public List<string> Monthly { get; set; }
        public List<string> TwiceMonthly { get; set; }
        public List<string> TwoWeeks { get; set; }
        public List<string> Weekly { get; set; }
        public List<string> Annually { get; set; }
    }
}