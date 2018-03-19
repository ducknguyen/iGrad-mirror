using IGrad.Models.Income;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IGrad.Context
{
    /// <summary>
    /// Family income fields on PDF. 
    /// </summary>
    public class IncomeContext : DbContext
    {
        public IncomeContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<FamilyIncome> Income { get; set; }
        

    }
}