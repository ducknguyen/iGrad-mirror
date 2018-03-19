using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGrad.Models.User
{
    public class LanguageHistory
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fieldId { get; set; }
        public Guid UserID { get; set; }

        [DisplayName("1. In what language(s) would your family prefer to communicate with school?")]
        public string PreferredLanguage { get; set; }

        [DisplayName("2. What language did the student learn first?")]
        public string UserFirstLanguageLearned { get; set; }

        [DisplayName("3. What language does the student use primarily at home?")]
        public string StudentPrimaryLanguageAtHome { get; set; }

        [DisplayName("4. What is the primary language used in the home, regardless of the language spoken by the student?")]
        public string PrimaryLanguageSpokenAtHome { get; set; }

        [DisplayName("5. Has the student received English language development supporting a previous school?")]
        public bool StudentReceievedEnglishDevelopmentSupport {get;set;}

        public bool unsureOfEnglishSupport { get; set; } // for Student recieved eng dev support, they have an unknown option


        [DisplayName("6. Has the student ever received formal education outside of the United States?")]
        public bool StudentHasReceivedFormalEducationOutsideUS { get; set; }

        //need to add months and language of instruction

        [DisplayName("7. When did the student first attend a school in the United States?")]
        public string StudentStartingGradeInUS { get; set; }
    }
}