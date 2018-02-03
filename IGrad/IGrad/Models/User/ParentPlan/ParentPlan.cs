namespace IGrad.Models.User
{
    public class ParentPlan
    {
        public bool inEffect { get; set; }
        public bool MotherHasOrder { get; set; }
        public bool FatherHasOrder { get; set; }
        public string Other { get; set; }
    }
}