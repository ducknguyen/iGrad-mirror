namespace IGrad.Models.User
{
    public class EmergencyContact
    {
        public Name Name { get; set; }
        public string Relationship { get; set; }
        public Phone[] PhoneNumbers { get; set; } 
    }
}