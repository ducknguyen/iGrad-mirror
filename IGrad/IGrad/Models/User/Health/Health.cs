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
        public string AllergiesDescription { get; set; }
        public bool Anaphylaxis { get; set; } //special
        public bool BeeInsectAllergy { get; set; }
        public string BeeInsectAllergyDescription { get; set; }
        public bool Asthma { get; set; } //special
        public bool BirthDefectsOrConcern { get; set; }
        public string BirthDefectsOrConcernDescription { get; set; }
        public bool FrequentEarInfenctions { get; set; }
        public string FrequentEarInfectionsDescription { get; set; }
        public bool HearingLoss { get; set; }
        public string HearingLossDescription { get; set; }
        public bool SpeechDifficulties { get; set; }
        public string SpeechDifficultiesDescription { get; set; }
        public bool SevereHeadaches { get; set; }
        public string SevereHeadachesDescription { get; set; }
        public bool Seizures { get; set; }
        public string SeizuresDescription { get; set; }
        public bool NeurologicalConditions { get; set; }
        public string NeurologicalConditionsDescription { get; set; }
        public bool ADDorADHD { get; set; }
        public string ADDorADHDDescription { get; set; }
        public bool HeartCondition { get; set; }
        public string HeartConditionDescription { get; set; }
        public bool Diabetes { get; set; } //special
        public bool BloodDisorder { get; set; }
        public string BloodDisorderDescription { get; set; }
        public bool OrthopedicCondition { get; set; }
        public string OrthopedicDescription { get; set; }
        public bool ChronicConditionOrDisability { get; set; }
        public string ChronicConditionOrDisabilityDescription { get; set; }
        public bool VisionConcerns { get; set; }
        public string VisionConcernsDescription { get; set; }
        public bool SeriousInjuryOrSurgery { get; set; }
        public string SeriousInjuryOrSurgeryDescription { get; set; }
        public bool EmotionalHealthConcerns { get; set; }
        public string EmotionalHealthConcernsDescription { get; set; }
        public bool OtherHealthConcerns { get; set; }
        public string OtherHealthConcernsDescription { get; set; }
    }   
}