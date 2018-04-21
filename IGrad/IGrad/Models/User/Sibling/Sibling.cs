using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class Sibling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        [DisplayName("Last Name")]
        public string LName { get; set; }
        [DisplayName("First Name")]
        public string FName { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public int Age { get; set; }
        [DisplayName("Sibling Is or Has Attended Kent School District")]
        public bool AttendedKentSchoolDistrict { get; set; }
    }
}