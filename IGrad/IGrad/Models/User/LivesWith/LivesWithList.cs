namespace IGrad.Models.User
{
    public class LivesWithList
    {
        public bool LivesWithBothParents { get; set; }
        public bool LivesWithMotherOnly { get; set; }
        public bool LivesWithFatherOnly { get; set; }
        public bool LivesWithGrandparents { get; set; }
        public bool LivesWithSelf { get; set; }
        public bool LivesWithFatherAndStepMom { get; set; }
        public bool LivesWithMotherAndStepDad { get; set; }
        public bool LivesWithFosterParents { get; set; }
        public bool LivesWithAgency { get; set; }
        public string AgencyName { get; set; }
        public string Other { get; set; }
    }
}