using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;

namespace HandlelisteAPI.Core.DataLogic;

public class DataLogicTests
{
    private readonly DbContextOptionsBuilder<HandlelisteContext> _builder;

    public DataLogicTests()
    {
        _builder = new DbContextOptionsBuilder<HandlelisteContext>();   
        _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    [Fact]
    public async void HandlelisteExistsReturnsCorrecBool()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var handleliste = new Handleliste() {
            HandlelisteName = "ListA",
            UserId = "1"
        };
        context.Handlelister.Add(handleliste);
        var hl = new HandlelisteLogic(context);
        await hl.SaveChangesAsync();

        var id1Exists = hl.HandlelisteExists(1);
        var id2Exists = hl.HandlelisteExists(2);

        Assert.True(id1Exists);
        Assert.False(id2Exists);
    }

    [Fact]
    public async void VareInstanceExistsReturnsCorrecBool()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var varer = new List<Vare>() {
            new Vare() { VareName = "A" },
            new Vare() { VareName = "B" }
        };
        context.Varer.AddRange(varer);
        var handleliste = new Handleliste() {
            HandlelisteName = "ListA",
            UserId = "1",
            Varer = new List<VareInstance>() { new VareInstance() { VareId = 1 } }
        };
        context.Handlelister.Add(handleliste);
        var hl = new HandlelisteLogic(context);
        await hl.SaveChangesAsync();

        var id1Exists = hl.VareInstanceExists(1, 1);
        var incorrectHandlelistId = hl.VareInstanceExists(2, 1);
        var incorrcetVareId = hl.VareInstanceExists(1, 2);

        Assert.True(id1Exists);
        Assert.False(incorrectHandlelistId);
        Assert.False(incorrcetVareId);
    }

    [Fact]
    public async void VareExistsReturnsCorrecBool()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var varer = new List<Vare>() {
            new Vare() { VareName = "A" },
            new Vare() { VareName = "B" }
        };
        context.Varer.AddRange(varer);
        var hl = new HandlelisteLogic(context);
        await hl.SaveChangesAsync();

        var id1Exists = hl.VareExists(1);
        var id2Exists = hl.VareExists(2);
        var id3Exists = hl.VareExists(3);

        Assert.True(id1Exists);
        Assert.True(id2Exists);
        Assert.False(id3Exists);
    }
}
