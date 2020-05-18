using KursAnaliz.Data.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KursAnaliz.Data.DataContext
{

    public class KursContext:DbContext
    {
     
        public DbSet<Kurslar> Kurslar { get; set; }
        public DbSet<Kursiyerler> Kursiyerler { get; set; }

        //Table oluşturulurken çoğul adla değilde benim verdiğim tekil isimle oluşsun
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
