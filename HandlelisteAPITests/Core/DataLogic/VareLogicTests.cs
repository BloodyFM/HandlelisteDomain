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
        _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    [Fact]
    public async void GetAllVarerDTOGetsAllVarer()
    {
        using var context = new HandlelisteContext(_builder.Options);
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

    [Fact]
    public async void GetVareByIdDTOGetsVareById()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var vareList = new List<Vare>() {
                new Vare() {VareName = "A" },
                new Vare() {VareName = "B" },
                new Vare() {VareName = "C" }
            };
        context.Varer.AddRange(vareList);
        var vl = new VareLogic(context);
        await vl.SaveChangesAsync();

        var vare = await vl.GetVareByIdDTO(2);
        var nullFetch = await vl.GetVareByIdDTO(4);

        Assert.NotNull(vare);
        Assert.Equal(2, vare.VareId);
        Assert.Equal("B", vare.VareName);
        Assert.Null(nullFetch);
    }

    [Fact]
    public async void GetHandlelisteIncludingVarerByIdGetsHandlelisteAndAllVareInsances()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var vareList = new List<Vare>() {
                new Vare() {VareName = "A" },
                new Vare() {VareName = "B" },
                new Vare() {VareName = "C" }
            };
        context.Varer.AddRange(vareList);
        var handleliste = new Handleliste()
        {
            HandlelisteName = "List",
            UserId = "1",
            Varer = new List<VareInstance>() {
                    new VareInstance() { VareId = 1, Mengde = 1 },
                    new VareInstance() { VareId = 2, Mengde = 3 },
                    new VareInstance() { VareId = 3, Mengde = 2 }
                }
        };
        context.Handlelister.Add(handleliste);
        var vl = new VareLogic(context);
        await vl.SaveChangesAsync();

        var fetchedHandleliste = await vl.GetHandlelisteIncludingVarerById(1);
        var nullFetch = await vl.GetHandlelisteIncludingVarerById(2);

        Assert.NotNull(fetchedHandleliste);
        Assert.Equal("List", fetchedHandleliste.HandlelisteName);
        Assert.NotNull(fetchedHandleliste.Varer);
        Assert.Equal(3, fetchedHandleliste.Varer.Count);
        Assert.Equal(1, fetchedHandleliste.Varer[0].Mengde);
        Assert.Equal(3, fetchedHandleliste.Varer[1].Mengde);
        Assert.Equal(2, fetchedHandleliste.Varer[2].Mengde);
        Assert.Null(nullFetch);
    }

    [Fact]
    public async void GetUsersHandlelisterThatContainsVareByIdGetsCorrectHandleliste()
    {
        using var context = new HandlelisteContext(_builder.Options);
        string user1 = "1";
        string user2 = "2";
        var vareList = new List<Vare>() {
                new Vare() {VareName = "A" },
                new Vare() {VareName = "B" },
                new Vare() {VareName = "C" }
            };
        context.Varer.AddRange(vareList);
        var handlelisteList = new List<Handleliste>()
            {
                new Handleliste() {
                    HandlelisteName = "List1",
                    UserId = user1,
                    Varer = new List<VareInstance>() {
                        new VareInstance() {VareId = 2, Mengde = 1},
                    },
                },
                new Handleliste() {
                    HandlelisteName = "List2",
                    UserId = user2,
                    Varer = new List<VareInstance>() {
                        new VareInstance() {VareId = 3, Mengde = 3},
                        new VareInstance() {VareId = 2, Mengde = 1},
                    },
                },
                new Handleliste() {
                    HandlelisteName = "List3",
                    UserId = user1,
                    Varer = new List<VareInstance>() {
                        new VareInstance() {VareId = 1, Mengde = 1},
                        new VareInstance() {VareId = 2, Mengde = 1}
                    }
                }
            };
        context.Handlelister.AddRange(handlelisteList);
        var vl = new VareLogic(context);
        await vl.SaveChangesAsync();

        var fetchedHandlelisterUser1VareId1 = await vl.GetUsersHandlelisterThatContainsVareById(user1, 1);
        var fetchedHandlelisterUser1VareId2 = await vl.GetUsersHandlelisterThatContainsVareById(user1, 2);
        var fetchedHandlelisterUser1VareId3 = await vl.GetUsersHandlelisterThatContainsVareById(user1, 3);

        Assert.NotNull(fetchedHandlelisterUser1VareId1);
        Assert.Single(fetchedHandlelisterUser1VareId1);
        Assert.NotNull(fetchedHandlelisterUser1VareId2);
        Assert.Equal(2, fetchedHandlelisterUser1VareId2.Count);
        Assert.NotNull(fetchedHandlelisterUser1VareId3);
        Assert.Empty(fetchedHandlelisterUser1VareId3);
    }
}
