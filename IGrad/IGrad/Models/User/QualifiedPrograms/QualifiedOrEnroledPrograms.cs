using System;
using System.ComponentModel;
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

        [DisplayName("Special Education")]
        public bool SpecialEducation { get; set; }

        [DisplayName("504")]
        public bool plan504 { get; set; }

        [DisplayName("Title")] //what is this?
        public bool Title { get; set; }

        [DisplayName("LAP")]
        public bool LAP { get; set; }

        [DisplayName("Highly Capable")]
        public bool HighlyCapable { get; set; }

        [DisplayName("English Language Learner")]
        public bool EngishAsSecondLanguage { get; set; }
    }
}