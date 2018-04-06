using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Last Year of School Attendance")]
        public int LastYearAttended { get; set; }
        [DisplayName("Is The Student Currently Expelled or Suspended From A School?")]
        public bool IsExpelledOrSuspended { get; set; }
        [DisplayName("Student's Current Grade")]
        public int CurrentGrade { get; set; }
        [DisplayName("Student's General Opinion of School")]
        public string SchoolOpinion { get; set; }
        [DisplayName("Student's Performance In School")]
        public string HowDoingInSchool { get; set; }
        [DisplayName("Student's Strengths and Areas For Improvement")]
        public string StrengthAndWeakness { get; set; }
        [DisplayName("Parental Comments")]
        public string ParentAdditionalFeedbackInfo { get; set; }
        public List<HighSchoolInfo> HighSchoolInformation { get; set; }
        public Violation PreviousSchoolViolation { get; set; }
        public PriorEducation PriorEducation { get; set; }
    }
}