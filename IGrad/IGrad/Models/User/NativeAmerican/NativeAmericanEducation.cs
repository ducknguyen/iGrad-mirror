using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User.NativeAmerican
{
    public class NativeAmericanEducation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }

        [DisplayName("Name of individual with Tribal enrollment")]
        public string NameOfIndividualWithTribalEnrollment { get; set; }
        
        [DisplayName("Child")]
        public bool TribalMembershipIsChilds { get; set; }
        [DisplayName("Child's Parent")]
        public bool TribalMembershipIsChildsParent { get; set; }
        [DisplayName("Child's Grandparent")]
        public bool TribalMembershipIsChildsGrandparent { get; set; }

        [DisplayName("Name of Band or Tribe which student claims membership")]
        public string NameOfTribeOrBandOfMembership { get; set; }
        [DisplayName("Federally recognized")]
        public bool TribeOrBandIsFederallyRecognized { get; set; }
        [DisplayName("State recognized")]
        public bool TribeOrBandIsStateRecognized { get; set; }
        [DisplayName("Terminated Tribe")]
        public bool TribeOrBandIsTerminatedTribe { get; set; }
        [DisplayName("Member of an organized Indian group")]
        public bool TribeOrBandIsOfIndianGroupEducationGrant { get; set; }

        [DisplayName("Membership or Enrollment Number (if available)")]
        public string MembershipOrEnrollmentNumber { get; set; }
        [DisplayName("Other Evidence of Membership in the tribe listed above")]
        public string DescriptionOfEvidenceOfEnrollment { get; set; }

        [DisplayName("Name of Tribe Managing Enrollment")]
        public string NameOfTribeMaintaningEnrollment { get; set; }
        [DisplayName("Address of Tribe Managing Enrollment")]
        public Address AddressOfTribeMaintainingEnrollment { get; set; }
    }
}