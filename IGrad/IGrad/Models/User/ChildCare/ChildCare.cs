namespace IGrad.Models.User
{
    public class ChildCare
    {
        public bool isBeforeSchool { get; set; }
        public bool isAfterSchool { get; set; }
        public string ProviderName { get; set; }
        public Address ProviderAddress { get; set; }
        public string ProviderPhoneNumber { get; set; }
    }
}