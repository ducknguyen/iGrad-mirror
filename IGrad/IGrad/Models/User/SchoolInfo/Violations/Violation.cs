using System;
using System.ComponentModel;
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
        [DisplayName("Student was suspended")]
        public bool isSuspendedOrExpelled { get; set; }
        // public bool beenRetained { get; set; } // dont think we need this as we have Retained
        [DisplayName("Was weapon violation")]
        public bool hadWeaponViolation { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of violation")]
        public DateTime dateOfWeaponViolation { get; set; }
        [DisplayName("Student has unpaid fines from violation")]
        public bool hasUnpaidFine { get; set; }
        [DisplayName("Reason for unpaid fine")]
        public string ExplainUnpaidFine { get; set; }
        [DisplayName("Student received disciplinary action")]
        public bool hasDiciplanaryStatus { get; set; }
        [DisplayName("Disciplinary action explanation")]
        public string ExplainDiciplanaryStatus { get; set; }
        [DisplayName("Student has Sex violation")]
        public bool hasSexViolation { get; set; }
        [DisplayName("Explain Sexual Violation")]
        public string explainSexViolation { get; set; }
        [DisplayName("Student has Criminal Violation")]
        public bool hasCriminalViolation { get; set; }
        [DisplayName("Explain Criminal Violation")]
        public string explainCriminalViolation { get; set; }
        [DisplayName("Student has Violent tendicies")]
        public bool hasViolentTendicies { get; set; }
        [DisplayName("Explain Violent Behavior")]
        public string explainViolence { get; set; }
        [DisplayName("Student has other violation")]
        public bool hasOtherViolation { get; set; }
        [DisplayName("Other violation explanation")]
        public string ExplainOtherViolation { get; set; }
 
    }
}