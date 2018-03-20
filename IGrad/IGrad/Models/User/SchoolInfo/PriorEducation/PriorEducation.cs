using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class PriorEducation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }

        [DisplayName("Has the Student Received Education Outside the U.S.?")]
        public bool hasEducationOutsideUS { get; set; }
        [DisplayName("How many months of education received?")]
        public int MonthsOfEducationOutsideUS { get; set; }
        [DisplayName("Language spoken during education received outside U.S.?")]
        public string LanguageOfEducationOutsideUS { get; set; }
        [DisplayName("Has the student previously attended schools in the U.S.?")]
        public bool AttendedSchoolinUS { get; set; }
    }
}