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

        // ************* ANAPHYLAXIS *************//
        [DisplayName("Anaphylaxis")]
        public bool Anaphylaxis { get; set; }
        [DisplayName("1. What is the student allergic to?")]
        public string AnaphylaxisAllerigicTo { get; set; }
        [DisplayName("2. What are the student's symptoms?")]
        public string AnaphylaxisSymptoms { get; set; }
        [DisplayName("3. Has the student been prescribed an Epi-Pen?")]
        public bool AnaphylaxisEpiPen { get; set; }

        [DisplayName("Bee/Insect Allergy")]
        public bool BeeInsectAllergy { get; set; }
        public string BeeInsectAllergyDescription { get; set; }

        // ************* ASTHMA *************//
        [DisplayName("Asthma")]
        public bool Asthma { get; set; }
        [DisplayName("1. How long has the student had asthma?")]
        public string HowLongAsthma { get; set; }
        [DisplayName("2. How many days would you eastimate the student missed school last year due to asthma?")]
        public string MissedSchoolAsthma { get; set; }
        [DisplayName("3. What are your student early warning signs of an asthma episode?")]
        public string EarlySignsAsthma { get; set; }
        [DisplayName("4. If your student asthma is monitored with a peak flow meter, write in their best peak flow rate")]
        public string BestPeakFlowRateAsthma { get; set; }
        [DisplayName("5. Does your student have and use a nebulizer machine at home?")]
        public bool HomeNebulizerAsthma { get; set; }
        [DisplayName("6. If your student takes medication for their asthma at home please provide the name of any medications:")]
        public string NameOfMedicationAsthma { get; set; }
        [DisplayName("None")]
        public bool AsthmaHospitalizedOvernightNone { get; set; }
        [DisplayName("Once")]
        public bool AsthmaHospitalizedOvernightOne { get; set; }
        [DisplayName("Two-Four")]
        public bool AsthmaHospitalizedOvernightTwoToFour { get; set; }
        [DisplayName("More Than Four")]
        public bool AsthmaHospitalizedOvernightMoreThanFour { get; set; }
        [DisplayName("None")]
        public bool AsthmaTreatedInEmergencyRoomNone { get; set; }
        [DisplayName("Once")]
        public bool AsthmaTreatedInEmergencyRoomOne { get; set; }
        [DisplayName("Two-Four")]
        public bool AsthmaTreatedInEmergencyRoomTwoToFour { get; set; }
        [DisplayName("More Than Four")]
        public bool AsthmaTreatedInEmergencyRoomMoreThanFour { get; set; }
        [DisplayName("None")]
        public bool AsthmaTreatedInDoctorsOfficeNone { get; set; }
        [DisplayName("Once")]
        public bool AsthmaTreatedInDoctorsOfficeOne { get; set; }
        [DisplayName("Two-Four")]
        public bool AsthmaTreatedInDoctorsOfficeTwoToFour { get; set; }
        [DisplayName("More Than Four")]
        public bool AsthmaTreatedInDoctorsOfficeMoreThanFour { get; set; }
        [DisplayName("Cough")]
        public bool AsthmaWarningSignCough { get; set; }
        [DisplayName("Cold Symptoms")]
        public bool AsthmaWarningSignColdSymptoms { get; set; }
        [DisplayName("Drop In Peak Flow")]
        public bool AsthmaWarningSignDropInPeakFlow { get; set; }
        [DisplayName("Wheezing")]
        public bool AsthmaWarningSignWheezing { get; set; }
        [DisplayName("Low Exercise")]
        public bool AsthmaWarningSignDecreasedExercise { get; set; }
        [DisplayName("Other")]
        public bool AsthmaWarningSignOther { get; set; }




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

        // ************* DIABETES *************//
        [DisplayName("Diabetes")]
        public bool Diabetes { get; set; } 

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