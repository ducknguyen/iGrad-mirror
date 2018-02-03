namespace IGrad.Models.User
{
    public class SchoolInfo
    {
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