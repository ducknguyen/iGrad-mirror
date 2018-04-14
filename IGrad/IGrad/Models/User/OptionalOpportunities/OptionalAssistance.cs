using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IGrad.Models.User.OptionalOpportunities
{
    public class OptionalAssistance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        [DisplayName("Student Is Homeless Or In Unstable Living Conditions")]
        public bool StudentIsHomelessOrUnstableLiving { get; set; }
        [DisplayName("Student Has Been In Foster Care")]
        public bool StudentHasBeenInFosterCare { get; set; }
        [DisplayName("Student Is Pregnant")]
        public bool StudentIsPregnant { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Due Date of Pregnancy")]
        public DateTime PregnantStudentDueDate { get; set; }
        [DisplayName("Student has Children")]
        public bool StudentIsParenting { get; set; }
        [DisplayName("Ages of Children")]
        public string AgesOfChilren { get; set;}
        [DisplayName("Student Has Been Involved With Court System")]
        public bool StudentHasBeenInvolvedWithCourt { get; set; }
        [DisplayName("Student Would Like Counselor or Social Worker to Contact Them")]
        public bool WouldLikeCounselorToContact { get; set; }
        [DisplayName("How Did You Hear About IGrad?")]
        public string HowStudentHeardAboutIGrad { get; set; }
    }
}