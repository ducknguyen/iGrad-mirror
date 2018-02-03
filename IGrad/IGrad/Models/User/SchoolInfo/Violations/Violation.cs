using System;

namespace IGrad.Models.User
{
    public class Violation
    {
        public bool isSuspendedOrExpelled { get; set; }
        // public bool beenRetained { get; set; } // dont think we need this as we have Retained
        public bool hadWeaponViolation { get; set; }
        public DateTime dateOfWeaponViolation { get; set; }
        public bool hasUnpaidFine { get; set; }
        public string ExplainUnpaidFine { get; set; }
        public bool hasDiciplanaryStatus { get; set; }
        public string ExplainDiciplanaryStatus { get; set; }
        public bool hasOtherViolation { get; set; }
        public string ExplainOtherViolation { get; set; }
 
    }
}