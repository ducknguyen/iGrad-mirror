using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class BirthPlaceLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }



    }
}