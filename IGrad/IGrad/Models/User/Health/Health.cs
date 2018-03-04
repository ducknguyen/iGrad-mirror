using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class Health
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public bool MedicationNeededAtSchool { get; set; }
        public string MedicationAtSchoolNames { get; set; }
        public bool MedicationNeededAtHome { get; set; }
        public string MedicationAtHomeNames { get; set; }
        public bool Allergies { get; set; }
        public bool Anaphylaxis { get; set; }
        public bool BeeInsectAllergy { get; set; }
        public bool Asthma { get; set; }
        public bool BirthDefectsOrConcern { get; set; }
        public bool FrequentEarInfenctions { get; set; }
        public bool HearingLoss { get; set; }
        public bool SpeechDifficulties { get; set; }
        public bool SevereHeadaches { get; set; }
        public bool Seizures { get; set; }
        public bool NeurologicalConditions { get; set; }
        public bool ADD { get; set; }
        public bool ADHD { get; set; }
        public bool HeartCondition { get; set; }
        public bool Diabetes { get; set; }
        public bool BloodDisorder { get; set; }
        public bool OrthopedicCondition { get; set; }
        public bool ChronicConditionOrDisability { get; set; }
        public bool VisionConcerns { get; set; }
        public bool SeriousInjuryOrSurgery { get; set; }
        public bool EmotionalHealthConcerns { get; set; }
        public bool OtherHealthConcerns { get; set; }
        public string OtherHealthConcern { get; set; }
    }   
}