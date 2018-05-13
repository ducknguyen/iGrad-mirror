using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class LivesWith
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        [DisplayName("Student Lives With Both Parents")]
        public bool LivesWithBothParents { get; set; }
        [DisplayName("Student Lives With Guardian")]
        public bool LivesWithGuardian { get; set; }
        [DisplayName("Student Lives With Mother Only")]
        public bool LivesWithMotherOnly { get; set; }
        [DisplayName("Student Lives With Father Only")]
        public bool LivesWithFatherOnly { get; set; }
        [DisplayName("Student Lives With Grandparents")]
        public bool LivesWithGrandparents { get; set; }
        [DisplayName("Student Provides Own Housing ")]
        public bool LivesWithSelf { get; set; }
        [DisplayName("Student Lives With Father and Step Mother")]
        public bool LivesWithFatherAndStepMom { get; set; }
        [DisplayName("Student Lives With Mother and Step Father")]
        public bool LivesWithMotherAndStepDad { get; set; }
        [DisplayName("Student Lives With Foster Parents")]
        public bool LivesWithFosterParents { get; set; }
        [DisplayName("Student Housing Provided By Agency")]
        public bool LivesWithAgency { get; set; }
        [DisplayName("Homeless / Distressed")]
        public bool HomelessDistressed { get; set; }
        [DisplayName("Total People In Residence")]
        public int TotalPeopleInResidence { get; set; }
        [DisplayName("Annual Household Income")]
        public int AnnualHouseHoldIncome { get; set; }
        [DisplayName("Name of Agency Providing Housing")]
        public string AgencyName { get; set; }
        public string Other { get; set; }
    }
}