using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class ParentPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public bool inEffect { get; set; }
        public bool MotherHasOrder { get; set; }
        public bool FatherHasOrder { get; set; }
        public string Other { get; set; }
    }
}