using System;
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
        public string LName { get; set; }
        public string FName { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public int Age { get; set; }
    }
}