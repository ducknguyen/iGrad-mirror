using System.Data.Entity;
using IGrad.Models.OpenHouse;

namespace IGrad.Context
{
    public class OpenHouseContext : DbContext
    {
        public OpenHouseContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<OpenHouse> OpenHouse { get; set; }
    }
}