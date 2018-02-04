using System;
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
        public string HighSchoolName { get; set; }
        public string HighSchoolYear { get; set; }
        public string HighSchoolCity { get; set; }
        public string HighSchoolState { get; set; }
        public bool isLastHighSchoolAttended { get; set; }
        public int HighSchoolGrade { get; set; }
    }
}