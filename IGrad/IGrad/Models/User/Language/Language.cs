using System;
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
        public string PreferredLanguage { get; set; }
        public string UserFirstLanguageLearned { get; set; }
        public string StudentPrimaryLanguageAtHome { get; set; }
        public string PrimaryLanguageSpokenAtHome { get; set; }
        public bool StudentReceievedEnglishDevelopmentSupport {get;set;}
        public bool unsureOfEnglishSupport { get; set; } // for Student recieved eng dev support, they have an unknown option
        public bool StudentHasReceivedFormalEducationOutsideUS { get; set; }
        public int StudentStartingGradeInUS { get; set; }
    }
}