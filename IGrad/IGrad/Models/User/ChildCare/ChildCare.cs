using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class ChildCare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public bool IsBeforeSchool { get; set; }
        public bool IsAfterSchool { get; set; }
        public bool IsBeforeAndAfterSchool { get; set; }
        public string ProviderName { get; set; }
        public Address ProviderAddress { get; set; }
        public string ProviderPhoneNumber { get; set; }
    }
}