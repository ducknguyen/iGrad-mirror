﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IGrad.Models.User
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        public Guid UserID { get; set; }
        public Name Name { get; set; }
        public string Email { get; set; }
        public LanguageHistory LanguageHisory { get; set; }
        public List<Guardian> Guardians { get; set; }
        public LivesWithList LivesWith { get; set; }
        public Date Birthday { get; set; }
        public BirthPlaceLocation BirthPlace { get; set; }
        public Address ResidentAddress { get; set; }
        public Address MailingAddress { get; set; }
        public Phone PhoneInfo { get; set; }
        public ParentPlan StudentsParentingPlan { get; set; }
        public List<Sibling> Siblings { get; set; }
        public ChildCare StudentChildCare { get; set; }
        public List<PreSchools> PreSchool { get; set; }
        public List<QualifiedOrEnroledPrograms> QualifiedOrEnrolledProgam { get; set; }
        public Retained Retainment { get; set; }
        public List<EmergencyContact> EmergencyContacts { get; set; }
        public RaceEthnicity ConsideredRaceAndEthnicity { get; set; }
        public string CountryBornIn { get; set; }
        public SchoolInfo SchoolInfo { get; set; }
        public Health HealthInfo { get; set; }

    }
}