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
    }

    [Fact]
    public async void GetHandlelisteByIdGetsCorrectHandleliste()
    {
        _builder.UseInMemoryDatabase("GetHandlelisteByIdGetsCorrectHandleliste");

        using var context = new HandlelisteContext(_builder.Options);
        var handlelisteList = new List<Handleliste>() {
            new Handleliste() { HandlelisteName = "ListA", UserId = "1"},
            new Handleliste() { HandlelisteName = "ListB", UserId = "1"},
            new Handleliste() { HandlelisteName = "ListC", UserId = "1"}
        };
        context.Handlelister.AddRange(handlelisteList);
        var vl = new HandlelisteLogic(context);
        await vl.SaveChangesAsync();

        var handleliste = await vl.GetHandlelisteById(3);
        var nullFetch = await vl.GetHandlelisteById(4);

        Assert.NotNull(handleliste);
        Assert.Equal("ListC", handleliste.HandlelisteName);
        Assert.Null(nullFetch);
    }

    [Fact]
    public async void GetHandlelisterByUserIdDTOReturnsCorrectNUmberOfHandlelister()
    {
        _builder.UseInMemoryDatabase("GetHandlelisterByUserIdDTOReturnsCorrectNUmberOfHandlelister");

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
        var vl = new HandlelisteLogic(context);
        await vl.SaveChangesAsync();

        var handlellisterUser1 = await vl.GetHandlelisterByUserIdDTO(User1);
        var handlellisterUser2 = await vl.GetHandlelisterByUserIdDTO(User2);
        var handlellisterUser3 = await vl.GetHandlelisterByUserIdDTO(User3);

        Assert.Equal(2, handlellisterUser1.Count);
        Assert.Single(handlellisterUser2);
        Assert.Empty(handlellisterUser3);
    }

    [Fact]
    public async void HandlelisteExistsReturnsCorrecBool()
    {
        _builder.UseInMemoryDatabase("HandlelisteExistsReturnsCorrecBool");

        using var context = new HandlelisteContext(_builder.Options);
        var handleliste = new Handleliste() {
            HandlelisteName = "ListA",
            UserId = "1"
        };
        context.Handlelister.Add(handleliste);
        var vl = new HandlelisteLogic(context);
        await vl.SaveChangesAsync();

        var id1Exists = vl.HandlelisteExists(1);
        var id2Exists = vl.HandlelisteExists(2);

        Assert.True(id1Exists);
        Assert.False(id2Exists);
    }
}
