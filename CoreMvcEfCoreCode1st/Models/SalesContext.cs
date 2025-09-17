using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;
using CoreMvcEfCoreCode1st.ViewModels;

namespace CoreMvcEfCoreCode1st.Models
{
    public class SalesContext : DbContext
    {
        //entities
        //public DbSet<SO> SOs { get; set; }
        //public DbSet<SoItem> SoItems { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<SubContact> SubContact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SalesDb;User Id=sa;Password=123;");
        }
    }
}
