using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;

namespace HandlelisteAPI;

public class DatabaseTests
{
    [Fact]
    public void CanInsertVareIntoDatabase()
    {
        var builder = new DbContextOptionsBuilder<HandlelisteContext>();
        //builder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = HandlelisteTestData");
        builder.UseInMemoryDatabase("CanInsertVareIntoDatabase");

        using (var context = new HandlelisteContext(builder.Options))
        {
            // resetting db is not neccessary with in memory db
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            var vare = new Vare {VareName = "A"};

            context.Varer.Add(vare);

            Assert.Equal(EntityState.Added, context.Entry(vare).State);
        }
    }
}
