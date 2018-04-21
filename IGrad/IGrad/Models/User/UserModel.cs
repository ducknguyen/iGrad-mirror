using IGrad.Models.User.NativeAmerican;
using IGrad.Models.User.HomelessAssistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IGrad.Models.User.OptionalOpportunities;
using System.ComponentModel;

namespace IGrad.Models.User
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        public Guid UserID { get; set; }
        public Name Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public LanguageHistory LanguageHisory { get; set; }
        public List<Guardian> Guardians { get; set; }
        public LivesWith LivesWith { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        public BirthPlaceLocation BirthPlace { get; set; }
        [DisplayName("Input Resident Address")]
        public Address ResidentAddress { get; set; }
        [DisplayName("Input Mailing Address")]
        public Address MailingAddress { get; set; }
        [DisplayName("Resident Address Same As Mailing?")]
        public bool ResidentAddressIsMailingAddress { get; set; }
        public Phone PhoneInfo { get; set; }
        public ParentPlan StudentsParentingPlan { get; set; }
        public List<Sibling> Siblings { get; set; }
        public ChildCare StudentChildCare { get; set; }
        public List<PreSchools> PreSchool { get; set; }
        public QualifiedOrEnrolledInProgram QualifiedOrEnrolledInProgam { get; set; }
        public Retained Retainment { get; set; }
        public List<EmergencyContact> EmergencyContacts{ get; set; }
        public RaceEthnicity ConsideredRaceAndEthnicity { get; set; }
        public NativeAmericanEducation NativeAmericanEducation { get; set; }
        public SchoolInfo SchoolInfo { get; set; }
        public Health HealthInfo { get; set; }
        public LifeEvent LifeEvent { get; set; }
        public Celebrate Celebrate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdateDate { get; set; }
        public bool isComplete { get; set; }
        public Roles role { get; set; }
        public HomelessAssistancePreferences HomelessAssistance { get; set; }
        public OptionalAssistance OptionalOpportunities { get; set; }
    }
}