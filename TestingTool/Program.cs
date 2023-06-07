using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

//using (HandlelisteContext context = new HandlelisteContext())
//{
//    context.Database.EnsureCreated();
//}


var _context = new HandlelisteContext();

//allqueries tested and working now to write it into an api

//works
//PostNewVare("Røkelaks");
void PostNewVare(string vareNavn)
{
    var vare = new Vare { VareName = vareNavn };
    _context.Varer.Add(vare);
    _context.SaveChanges();
}

//works
var varer = new List<VareInstance>()
{
    new VareInstance { VareId = 1, Mengde = 2 },
    new VareInstance { VareId = 2, Mengde = 3 },
    new VareInstance { VareId = 3, Mengde = 2 }
};
//PostNewHandleliste("1", "Pizzakveld", varer);
void PostNewHandleliste(string userId, string name, List<VareInstance> varer)
{
    var handleliste = new Handleliste { UserId = userId, HandlelisteName = name, Varer = varer};
    _context.Handlelister.Add(handleliste);
    _context.SaveChanges();
}

//works
var updatedVarer = new List<VareInstance>()
{
    new VareInstance { VareId = 1, Mengde = 3 },
    new VareInstance { VareId = 2, Mengde = 6 },
    new VareInstance { VareId = 3, Mengde = 3 }
};
var vareIsdeleted = new List<bool>() { false, false, false };
//UpdateHandleliste(1, "Pizza Kveld", updatedVarer, vareIsdeleted);
void UpdateHandleliste(int id, string updatedName, List<VareInstance> updatedVarer, List<bool> vareIsDeleted)
{
    var handleliste = _context.Handlelister.Find(id);
    var vareInstances = _context.Vareinstance.Where(vi => vi.HandlelisteId == id).ToList();
    if (handleliste != null)
    {
        if (vareInstances.Count > 0)
        {
            for (int i = 0; i < vareInstances.Count; i++)
            {
                if (vareIsDeleted[i])
                    _context.Vareinstance.Remove(vareInstances[i]);
                else
                    vareInstances[i].Mengde = updatedVarer[i].Mengde;
            }
            if (updatedVarer.Count > vareInstances.Count)
            {
                for (int i = vareInstances.Count; i < updatedVarer.Count; i++)
                {
                    handleliste.Varer.Add(updatedVarer[i]);
                }
            }
        }
        else
        {
            handleliste.Varer = updatedVarer;
        }
        handleliste.HandlelisteName = updatedName;
        _context.SaveChanges();
    }
}

//works
//GetHandlelisterWithUserId("1");
void GetHandlelisterWithUserId(string userId)
{
    var handlelister = _context.Handlelister.Where(h => h.UserId == userId).ToList();
    if (handlelister != null)
    {
        foreach (var handleliste in handlelister)
        {
            Console.WriteLine($"User {userId} har handlelisten '{handleliste.HandlelisteName}' den har id: {handleliste.HandlelisteId}");
        }
    }
}

//works
//GetVarerThatStartsWith("Rø");
void GetVarerThatStartsWith(string vareNavn)
{
    var varer = _context.Varer.Where(v => v.VareName.StartsWith($"{vareNavn}")).OrderBy(v => v.VareName).ToList();

    foreach (var vare in varer)
    {
        Console.WriteLine($"{vareNavn} returns: {vare.VareName}");
    }
}

//works
//GetHandlelisteWithUserId(1);
void GetHandlelisteWithUserId(int handlelisteId)
{
    var handleliste = _context.Handlelister
        .Include(h => h.Varer)
        .ThenInclude(vi => vi.Vare)
        .FirstOrDefault(h => h.HandlelisteId == handlelisteId);
    if (handleliste != null)
    {
        Console.WriteLine($"Handleliste {handleliste.HandlelisteName}:");
        foreach(var vare in handleliste.Varer)
        {
            Console.WriteLine($"{vare.Mengde}x  {vare.Vare.VareName}");
        }
    }
}
