using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class Phone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public string PhoneNumber { get; set; }
        public bool isHomePhone { get; set; }
        public bool isCellPhone { get; set; }
        public bool isWorkPhone { get; set; }
    }
}