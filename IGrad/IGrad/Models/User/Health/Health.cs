using System;
using System.ComponentModel;
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

    
        [DisplayName("Medication needed at school")]
        public bool MedicationNeededAtSchool { get; set; }
        public string MedicationAtSchoolNames { get; set; }
        [DisplayName("Medication needed at home")]
        public bool MedicationNeededAtHome { get; set; }
        public string MedicationAtHomeNames { get; set; }

        [DisplayName("Allergies")]
        public bool Allergies { get; set; }
        public string AllergiesDescription { get; set; }

        [DisplayName("Anaphylaxis")]
        public bool Anaphylaxis { get; set; } //special

        [DisplayName("Bee/Insect Allergy")]
        public bool BeeInsectAllergy { get; set; }
        public string BeeInsectAllergyDescription { get; set; }

        [DisplayName("Asthma")]
        public bool Asthma { get; set; } //special

        [DisplayName("Birth defects")]
        public bool BirthDefectsOrConcern { get; set; }
        public string BirthDefectsOrConcernDescription { get; set; }

        [DisplayName("Frequent Ear Infections")]
        public bool FrequentEarInfections { get; set; }
        public string FrequentEarInfectionsDescription { get; set; }

        [DisplayName("Hearing loss")]
        public bool HearingLoss { get; set; }
        public string HearingLossDescription { get; set; }

        [DisplayName("Speech difficulties")]
        public bool SpeechDifficulties { get; set; }
        public string SpeechDifficultiesDescription { get; set; }

        [DisplayName("Severe headaches")]
        public bool SevereHeadaches { get; set; }
        public string SevereHeadachesDescription { get; set; }

        [DisplayName("Seizures")]
        public bool Seizures { get; set; }
        public string SeizuresDescription { get; set; }

        [DisplayName("Neurological conditions")]
        public bool NeurologicalConditions { get; set; }
        public string NeurologicalConditionsDescription { get; set; }

        [DisplayName("ADD/ADHD")]
        public bool ADDorADHD { get; set; }
        public string ADDorADHDDescription { get; set; }

        [DisplayName("Heart condition")]
        public bool HeartCondition { get; set; }
        public string HeartConditionDescription { get; set; }

        [DisplayName("Diabetes")]
        public bool Diabetes { get; set; } //special

        [DisplayName("Blood disorder")]
        public bool BloodDisorder { get; set; }
        public string BloodDisorderDescription { get; set; }

        [DisplayName("Orthopedic condition")]
        public bool OrthopedicCondition { get; set; }
        public string OrthopedicConditionDescription { get; set; }

        [DisplayName("Chronic condition/disability")]
        public bool ChronicConditionOrDisability { get; set; }
        public string ChronicConditionOrDisabilityDescription { get; set; }

        [DisplayName("Vision concerns")]
        public bool VisionConcerns { get; set; }
        public string VisionConcernsDescription { get; set; }

        [DisplayName("Serious injury or surgery")]
        public bool SeriousInjuryOrSurgery { get; set; }
        public string SeriousInjuryOrSurgeryDescription { get; set; }

        [DisplayName("Emotional health concerns")]
        public bool EmotionalHealthConcerns { get; set; }
        public string EmotionalHealthConcernsDescription { get; set; }

        [DisplayName("Other health concerns")]
        public bool OtherHealthConcerns { get; set; }
        public string OtherHealthConcernsDescription { get; set; }
    }   
}