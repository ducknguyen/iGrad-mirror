using IGrad.Models.User;
using IGrad.Models.User.NativeAmerican;
using System.Data.Entity;


namespace IGrad.Context
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<HighSchoolInfo> HighSchoolInfoes { get; set; }

        public DbSet<Health> Healths { get; set; }

        public DbSet<Guardian> Guardians { get; set; }
        
        public DbSet<Violation> Violations { get; set; }

        public DbSet<Sibling> Siblings { get; set; }

        public DbSet<EmergencyContact> EmergencyContacts { get; set; }

        public DbSet<NativeAmericanEducation> NativeAmericanEducations { get; set; }

        public System.Data.Entity.DbSet<IGrad.Models.User.HomelessAssistance.HomelessAssistancePreferences> HomelessAssistancePreferences { get; set; }

        public System.Data.Entity.DbSet<IGrad.Models.User.OptionalOpportunities.OptionalAssistance> IGradOpportunities { get; set; }
    }
}