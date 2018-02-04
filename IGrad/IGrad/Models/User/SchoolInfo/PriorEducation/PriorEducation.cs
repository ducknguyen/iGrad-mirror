using System;
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
        public bool hasEducationOutsideUS { get; set; }
        public int MonthsOfEducationOutsideUS { get; set; }
        public string LanguageOfEducationOutsideUS { get; set; }
        public bool AttendedSchoolinUS { get; set; }
    }
}