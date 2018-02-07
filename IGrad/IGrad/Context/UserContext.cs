using IGrad.Models.User;
using System.Data.Entity;

namespace IGrad.Context
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection") { }
        public DbSet<UserModel> Users { get; set; }
    }
}