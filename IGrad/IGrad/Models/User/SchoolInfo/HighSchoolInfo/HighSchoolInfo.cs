using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class HighSchoolInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        [DisplayName("Highschool Name")]
        public string HighSchoolName { get; set; }
        [DisplayName("Last Year Attended")]
        public string HighSchoolYear { get; set; }
        [DisplayName("City of Highschool")]
        public string HighSchoolCity { get; set; }
        [DisplayName("State of Highschool")]
        public string HighSchoolState { get; set; }
        [DisplayName("Last Highschool Attended")]
        public bool isLastHighSchoolAttended { get; set; }
        [DisplayName("Last Grade of Attendance")]
        public int HighSchoolGrade { get; set; }
    }
}