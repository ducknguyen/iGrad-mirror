using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IGrad.Models.User
{
    public class UserModel
    {
        public DateTime Birthday { get; set; }
        public BirthPlaceLocation BirthPlace { get; set; }
        public LoginViewModel LoginInfo { get; set; }
        public Address ResidentAddress { get; set; }
        public Address MailingAddress { get; set; }
        public HomePhone PhoneObject { get; set; }
        public ParentPlan StudentsParentingPlan { get; set; }
        public Sibling[] Siblings { get; set; }
        public ChildCare StudentChildCare { get; set; }
        public PreSchools[] PreSchool { get; set; }
        public QualifiedOrEnroledPrograms[] QualifiedOrEnrolledProgam { get; set; }
        public Retained Retainment { get; set; }
        public EmergencyContact[] EmergencyContacts { get; set; }
        public RaceEthnicity ConsideredRaceAndEthnicity { get; set; }
        public string CountryBornIn { get; set; }
        public SchoolInfo HighSchoolInfo { get; set; }
        public Health HealthInfo { get; set; }

    }
}