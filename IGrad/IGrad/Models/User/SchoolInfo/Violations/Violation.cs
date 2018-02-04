using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class Violation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public bool isSuspendedOrExpelled { get; set; }
        // public bool beenRetained { get; set; } // dont think we need this as we have Retained
        public bool hadWeaponViolation { get; set; }
        public Date dateOfWeaponViolation { get; set; }
        public bool hasUnpaidFine { get; set; }
        public string ExplainUnpaidFine { get; set; }
        public bool hasDiciplanaryStatus { get; set; }
        public string ExplainDiciplanaryStatus { get; set; }
        public bool hasOtherViolation { get; set; }
        public string ExplainOtherViolation { get; set; }
 
    }
}