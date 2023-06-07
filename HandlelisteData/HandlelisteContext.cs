using Microsoft.EntityFrameworkCore;
using HandlelisteDomain;

namespace HandlelisteData
{
    public class HandlelisteContext:DbContext
    {
        public HandlelisteContext()
        {
        
        }
        public HandlelisteContext(DbContextOptions<HandlelisteContext> options)
            : base(options)
        {

        }
        public DbSet<Vare> Varer { get; set; }
        public DbSet<VareInstance> Vareinstance { get; set; }
        public DbSet<Handleliste> Handlelister { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = HandlelisteDatabase");
            }
            //optionsBuilder.UseSqlServer(
            //    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = HandlelisteDatabase"
            //).LogTo(Console.WriteLine,
            //        new[] { DbLoggerCategory.Database.Command.Name },
            //        LogLevel.Information)
            //.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var VareList = new Vare[]
            {
                new Vare {VareId = 1, VareName = "Pizza"},
                new Vare {VareId = 2, VareName = "Cola"},
                new Vare {VareId = 3, VareName = "Rømmedressing"},
                new Vare {VareId = 4, VareName = "Rømme"}
            };
            modelBuilder.Entity<Vare>().HasData(VareList);

            modelBuilder.Entity<VareInstance>().HasKey(vi => new { vi.VareId, vi.HandlelisteId });
        }
    }
}