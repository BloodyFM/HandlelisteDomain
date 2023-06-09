using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;

namespace HandlelisteAPI.Core.DataLogic;

public class HandlelisteWithVarerLogicTests
{
    private readonly DbContextOptionsBuilder<HandlelisteContext> _builder;

    public HandlelisteWithVarerLogicTests()
    {
        _builder = new DbContextOptionsBuilder<HandlelisteContext>();   
        _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    [Fact]
    public async void GetVareInstanceByIdDTOReturnCorrctData()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var hwvl = new HandlelisteWithVarerLogic(context);
        var vareList = new List<Vare>() {
            new Vare() { VareName = "A" },
            new Vare() { VareName = "B" },
            new Vare() { VareName = "C" }
        };
        context.Varer.AddRange(vareList);
        var handleliste = new Handleliste() {
            HandlelisteName = "Liste1",
            UserId = "1",
            Varer = new List<VareInstance>() {
                new VareInstance() { VareId = 2, Mengde = 1 }
            }
        };
        context.Add(handleliste);
        await hwvl.SaveChangesAsync();


        var vareInstance = await hwvl.GetVareInstanceByIdDTO(1, 2);
        var wrongVareId = await hwvl.GetVareInstanceByIdDTO(1, 3);
        var wrongHandlelisteId = await hwvl.GetVareInstanceByIdDTO(2, 2);

        Assert.NotNull(vareInstance);
        Assert.Equal(1, vareInstance.Mengde);
        Assert.Null(wrongVareId);
        Assert.Null(wrongHandlelisteId);
    }

    [Fact]
    public async void GetVareInstancesByHandlelisteIdReturnsCorrectNumberOfInsances()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var hwvl = new HandlelisteWithVarerLogic(context);
        var vareList = new List<Vare>() {
            new Vare() { VareName = "A" },
            new Vare() { VareName = "B" },
            new Vare() { VareName = "C" }
        };
        context.Varer.AddRange(vareList);
        var handleliste = new Handleliste() {
            HandlelisteName = "Liste1",
            UserId = "1",
            Varer = new List<VareInstance>() {
                new VareInstance() { VareId = 2, Mengde = 1 },
                new VareInstance() { VareId = 3, Mengde = 3 }
            }
        };
        context.Add(handleliste);
        await hwvl.SaveChangesAsync();

        var instances = await hwvl.GetVareInstancesByHandlelisteId(1);
        var incorrectId = await hwvl.GetVareInstancesByHandlelisteId(2);

        Assert.Equal(2, instances.Count);
        Assert.Equal(1, instances[0].Mengde);
        Assert.Equal(3, instances[1].Mengde);
        Assert.Empty(incorrectId);
    }

    [Fact]
    public async void GetVareInstanceByHandlelisteIdAndVareIdReturnsNullOrInstance()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var hwvl = new HandlelisteWithVarerLogic(context);
        var vareList = new List<Vare>() {
            new Vare() { VareName = "A" },
            new Vare() { VareName = "B" },
            new Vare() { VareName = "C" }
        };
        context.Varer.AddRange(vareList);
        var handleliste = new Handleliste() {
            HandlelisteName = "Liste1",
            UserId = "1",
            Varer = new List<VareInstance>() {
                new VareInstance() { VareId = 2, Mengde = 1 }
            }
        };
        context.Add(handleliste);
        await hwvl.SaveChangesAsync();

        var instance = await hwvl.GetVareInstanceByHandlelisteIdAndVareId(1, 2);
        var incorrectHandlelisteId = await hwvl.GetVareInstanceByHandlelisteIdAndVareId(2, 2);
        var incorrectVareId = await hwvl.GetVareInstanceByHandlelisteIdAndVareId(1, 1);

        Assert.NotNull(instance);
        Assert.Equal(1, instance.Mengde);
        Assert.Null(incorrectHandlelisteId);
        Assert.Null(incorrectVareId);
    }

    [Fact]
    public async void GetHandlelisteIncludeVarerReturnsNullOrHandlelisteWithWarerData()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var hwvl = new HandlelisteWithVarerLogic(context);
        var vareList = new List<Vare>() {
            new Vare() { VareName = "A" },
            new Vare() { VareName = "B" },
            new Vare() { VareName = "C" }
        };
        context.Varer.AddRange(vareList);
        var handleliste = new Handleliste() {
            HandlelisteName = "ListeName",
            UserId = "1",
            Varer = new List<VareInstance>() {
                new VareInstance() { VareId = 1, Mengde = 3 },
                new VareInstance() { VareId = 2, Mengde = 1 }
            }
        };
        context.Add(handleliste);
        await hwvl.SaveChangesAsync();

        var fetchedHandleliste = await hwvl.GetHandlelisteIncludeVarer(1);
        var invalidId = await hwvl.GetHandlelisteIncludeVarer(2);

        Assert.NotNull(fetchedHandleliste);
        Assert.Equal("ListeName", fetchedHandleliste.HandlelisteName);
        Assert.NotNull(fetchedHandleliste.Varer);
        Assert.Equal(2, fetchedHandleliste.Varer.Count);
        Assert.Equal(vareList[0] ,fetchedHandleliste.Varer[0].Vare);
        Assert.Equal(vareList[1], fetchedHandleliste.Varer[1].Vare);
        Assert.Equal(3, fetchedHandleliste.Varer[0].Mengde);
        Assert.Equal(1, fetchedHandleliste.Varer[1].Mengde);
        Assert.Null(invalidId);
    }
}
