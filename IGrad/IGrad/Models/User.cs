using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IGrad.Models
{
    public class User
    {
        public int UserID;
        public ImmunizationRecord[] iRecords { get; set; }
        public string FirstName;
        public string LastName;
        public string StreetAddress;
        public string City;
        public string PostalCode;

    }
}