﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }
        public string Street { get; set; }
        public string AptNum { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string POBox { get; set; }

        public string PrintAddress()
        {
            string addressString = "";

            if(string.IsNullOrEmpty(POBox))
            {
                if (string.IsNullOrEmpty(AptNum))
                {
                    addressString = Street + ", " + City + ", " + State + ", " + Zip;
                }
                else
                {
                    addressString = Street + ", Apt# " + AptNum + ", " + City + ", " + State + ", " + Zip;
                }
            }
            else
            {
                addressString = "POBOX " + POBox + ", " + City + ", " + State + ", " + Zip;
            }

            return addressString;
        }
    }
}