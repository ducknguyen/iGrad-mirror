using System;
using System.ComponentModel;
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
        [DisplayName("Parenting Plan In Effect")]
        public bool inEffect { get; set; }
        [DisplayName("Mother Has Ordered Parenting Plan")]
        public bool MotherHasOrder { get; set; }
        [DisplayName("Father Has Ordered Parenting Plan")]
        public bool FatherHasOrder { get; set; }
        [DisplayName("Other Parenting Plan")]
        public string Other { get; set; }
        [DisplayName("Court Order Limiting Educational Decisions In Effect")]
        public bool CourtOrderOnEducationDecisions { get; set; }
    }
}