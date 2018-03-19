using IGrad.Models.User;
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
    }
}