using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class SchoolInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public int LastYearAttended { get; set; }
        public int CurrentGrade { get; set; }
        public string SchoolOpinion { get; set; }
        public string HowDoingInSchool { get; set; }
        public string StrengthAndWeakness { get; set; }
        public string ParentAdditionalFeedbackInfo { get; set; }
        public HighSchoolInfo[] HighSchoolInformation { get; set; }
        public Violation PreviousSchoolViolation { get; set; }
        public PriorEducation PriorEducation { get; set; }
    }
}