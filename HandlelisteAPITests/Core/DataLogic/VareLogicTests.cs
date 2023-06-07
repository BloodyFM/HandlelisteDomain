using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;

namespace HandlelisteAPI.Core.DataLogic;

public class VareLogicTests
{
    private readonly DbContextOptionsBuilder<HandlelisteContext> _builder;

    public VareLogicTests()
    {
        _builder = new DbContextOptionsBuilder<HandlelisteContext>();
    }

    [Fact]
    public async void GetAllVarerDTOGetsAllVarer()
    {
        _builder.UseInMemoryDatabase("GetAllVarerDTOGetsAllVarer");

        using (var context = new HandlelisteContext(_builder.Options))
        {
            var vareList = new List<Vare>() {
                new Vare() {VareName = "A" },
                new Vare() {VareName = "B" },
                new Vare() {VareName = "C" }
            };
            context.Varer.AddRange(vareList);
            var vl = new VareLogic(context);
            await vl.SaveChangesAsync();

            var varer = await vl.GetAllVarerDTO();
            
            Assert.Equal(vareList.Count, varer.Count);
        }
    }

    [Fact]
    public async void GetVareByIdDTOGetsVareById()
    {
        _builder.UseInMemoryDatabase("GetVareByIdDTOGetsVareById");

        using (var context = new HandlelisteContext(_builder.Options))
        {
            var vareList = new List<Vare>() {
                new Vare() {VareName = "A" },
                new Vare() {VareName = "B" },
                new Vare() {VareName = "C" }
            };
            context.Varer.AddRange(vareList);
            var vl = new VareLogic(context);
            await vl.SaveChangesAsync();

            var vare = await vl.GetVareByIdDTO(2);
            
            Assert.NotNull(vare);
            Assert.Equal(2, vare.VareId);
            Assert.Equal("B", vare.VareName);
        }
    }
}
