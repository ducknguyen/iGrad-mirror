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

        [DisplayName("Name Of Individual With Tribal Enrollment")]
        public string NameOfIndividualWithTribalEnrollment { get; set; }
        [DisplayName("Membership is Student's")]
        public bool TribalMembershipIsChilds { get; set; }
        [DisplayName("Membership is Student's Parent")]
        public bool TribalMembershipIsChildsParent { get; set; }
        [DisplayName("Membership is Student's Grandparent")]
        public bool TribalMembershipIsChildsGrandparent { get; set; }
        [DisplayName("Name of Band or Tribe")]
        public string NameOfTribeOrBandOfMembership { get; set; }
        [DisplayName("Tribe is Federally Recognized?")]
        public bool TribeOrBandIsFederallyRecognized { get; set;}
        [DisplayName("Tribe is State Recognized?")]
        public bool TribeOrBandIsStateRecognized { get; set; }
        [DisplayName("Tribe or Tribe has been Terminated ")]
        public bool TribeOrBandIsTerminatedTribe { get; set; }
        [DisplayName("Tribe has Received Education Grant under the Education Act of 1988?")]
        public bool TribeOrBandIsOfIndianGroupEducationGrant { get; set; }
        [DisplayName("Membership or Enrollment Number")]
        public string MembershipOrEnrollmentNumber { get; set; }
        [DisplayName("Description of Evidence of Tribal Membership")]
        public string DescriptionOfEvidenceOfEnrollment { get; set; }
        [DisplayName("Name of Tribe Managing Enrollment")]
        public string NameOfTribeMaintaningEnrollment { get; set; }
        [DisplayName("Address of Tribe Managing Enrollment")]
        public Address AddressOfTribeMaintainingEnrollment { get; set; }
    }
}