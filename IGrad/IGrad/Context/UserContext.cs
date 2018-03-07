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

        public System.Data.Entity.DbSet<IGrad.Models.User.HighSchoolInfo> HighSchoolInfoes { get; set; }

        public System.Data.Entity.DbSet<IGrad.Models.User.Health> Healths { get; set; }

        public System.Data.Entity.DbSet<IGrad.Models.User.Guardian> Guardians { get; set; }
    }
}