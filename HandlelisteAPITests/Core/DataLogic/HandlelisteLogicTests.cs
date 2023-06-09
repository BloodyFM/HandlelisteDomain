using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;

namespace HandlelisteAPI.Core.DataLogic;

public class HandlelisteLogicTests
{
    private readonly DbContextOptionsBuilder<HandlelisteContext> _builder;

    public HandlelisteLogicTests()
    {
        _builder = new DbContextOptionsBuilder<HandlelisteContext>();   
        _builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    [Fact]
    public async void GetHandlelisteByIdGetsCorrectHandleliste()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var handlelisteList = new List<Handleliste>() {
            new Handleliste() { HandlelisteName = "ListA", UserId = "1"},
            new Handleliste() { HandlelisteName = "ListB", UserId = "1"},
            new Handleliste() { HandlelisteName = "ListC", UserId = "1"}
        };
        context.Handlelister.AddRange(handlelisteList);
        var hl = new HandlelisteLogic(context);
        await hl.SaveChangesAsync();

        var handleliste = await hl.GetHandlelisteById(3);
        var nullFetch = await hl.GetHandlelisteById(4);

        Assert.NotNull(handleliste);
        Assert.Equal("ListC", handleliste.HandlelisteName);
        Assert.Null(nullFetch);
    }

    [Fact]
    public async void GetHandlelisterByUserIdDTOReturnsCorrectNUmberOfHandlelister()
    {
        using var context = new HandlelisteContext(_builder.Options);
        var User1 = "1";
        var User2 = "2";
        var User3 = "3";
        var handlelisteList = new List<Handleliste>() {
            new Handleliste() { HandlelisteName = "ListA", UserId = "1"},
            new Handleliste() { HandlelisteName = "ListB", UserId = "2"},
            new Handleliste() { HandlelisteName = "ListC", UserId = "1"}
        };
        context.Handlelister.AddRange(handlelisteList);
        var hl = new HandlelisteLogic(context);
        await hl.SaveChangesAsync();

        var handlellisterUser1 = await hl.GetHandlelisterByUserIdDTO(User1);
        var handlellisterUser2 = await hl.GetHandlelisterByUserIdDTO(User2);
        var handlellisterUser3 = await hl.GetHandlelisterByUserIdDTO(User3);

        Assert.Equal(2, handlellisterUser1.Count);
        Assert.Single(handlellisterUser2);
        Assert.Empty(handlellisterUser3);
    }
}
