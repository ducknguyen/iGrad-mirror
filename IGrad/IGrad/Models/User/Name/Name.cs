using System.ComponentModel;

namespace IGrad.Models.User
{
    public class Name
    {
        [DisplayName("First Name")]
        public string FName { get; set; }
        [DisplayName("Last Name")]
        public string LName { get; set; }
        [DisplayName("Middle Name")]
        public string MName { get; set; }
        [DisplayName("Nick Name")]
        public string NickName { get; set; }
        [DisplayName("Previous Name")]
        public string PreviousName { get; set; }
    }
}