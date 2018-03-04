using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class QualifiedOrEnrolledInProgram
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public bool SpecialEducation { get; set; }
        public bool plan504 { get; set; }
        public bool Title { get; set; }
        public bool LAP { get; set; }
        public bool HighlyCapable { get; set; }
        public bool EngishAsSecondLanguage { get; set; }
    }
}