using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User.HomelessAssistance
{
    public class HomelessAssistancePreferences
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        [DisplayName("Contact With Benefits By Phone")]
        public bool ContactByPhone { get; set; }
        [DisplayName("Contact With Benefits By Email")]
        public bool ContactByEmail { get; set; }
        [DisplayName("Contact With Benefits By Note")]
        public bool ContactByStudentNote { get; set; }

    }
}