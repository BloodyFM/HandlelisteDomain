using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HandlelisteAPI.DataLogic
{
    public class HandlelisteWithVarerLogic : DataLogic
    {
        public HandlelisteWithVarerLogic(HandlelisteContext context)
            : base(context)
        {
        }

        public async Task<VareInstanceDTO?> GetVareInstanceByIdDTO(int handlelisteId, int vareId)
        {
            var vareInstance = await _context.Vareinstance.Include(vi => vi.Vare).FirstOrDefaultAsync(vi => vi.HandlelisteId == handlelisteId && vi.VareId == vareId);
            if (vareInstance == null)
            {
                return null;
            }
            return VareInstanceToDTO(vareInstance, vareInstance.Vare.VareName);
        }

        public async Task<Handleliste?> FindHandlelisteById(int id)
        {
            return await _context.Handlelister.FindAsync(id);
        }

        public async Task<List<VareInstance>> FindVareInstancesByHandlelisteId(int handlelisteId)
        {
            return await _context.Vareinstance.Where(vi => vi.HandlelisteId == handlelisteId).ToListAsync();
        }

        public async Task<VareInstance?> GetVareInstanceByHandlelisteIdAndVareId(int handlelisteId, int vareId)
        {
            return await _context.Vareinstance.FirstOrDefaultAsync(vi => vi.HandlelisteId == handlelisteId && vi.VareId == vareId);
        }

        public async Task<Handleliste?> GetHandlelisteIncludeVarer(int handlelisteId)
        {
            return await _context.Handlelister
                .Include(h => h.Varer)
                .ThenInclude(vi => vi.Vare)
                .FirstOrDefaultAsync(h => h.HandlelisteId == handlelisteId);
        }

        public void SetModifiedVareInstanceState(VareInstance vareInstance)
        {
            _context.Entry(vareInstance).State = EntityState.Modified;
        }

        public bool VareInstanceExists(int handlelisteId, int vareId)
        {
            return (_context.Vareinstance?.Any(vi => vi.HandlelisteId == handlelisteId && vi.VareId == vareId)).GetValueOrDefault();
        }
        public bool VareExists(int vareId)
        {
            return (_context.Varer?.Any(v => v.VareId == vareId)).GetValueOrDefault();
        }

        public EntityEntry<Handleliste> AddHandleliste(Handleliste handleliste)
        {
            return _context.Handlelister.Add(handleliste);
        }

        public void AddVareInstance(VareInstance vareInstance)
        {
            _context.Vareinstance.Add(vareInstance);
        }

        public void RemoveVareInstance(VareInstance vareInstance)
        {
            _context.Vareinstance.Remove(vareInstance);
        }

        public void RemoveHandleliste(Handleliste handleliste)
        {
            _context.Handlelister.Remove(handleliste);
        }

        public bool HandlelisteExists(int id)
        {
            return (_context.Handlelister?.Any(e => e.HandlelisteId == id)).GetValueOrDefault();
        }

        public Handleliste HandlelisteWithVarerFromDTO(HandlelisteWithVarerDTO handleliste)
        {
            var varer = handleliste.Varer
                .Select(v => new VareInstance
                {
                    VareId = v.VareId,
                    Mengde = v.Mengde,
                    IsCollected = v.IsCollected,
                }).ToList();
            var NewHandleliste = new Handleliste()
            {
                UserId = handleliste.UserId,
                HandlelisteName = handleliste.HandlelisteName,
                Varer = varer
            };

            return NewHandleliste;
        }

        public HandlelisteWithVarerDTO HandlelisteWithVarerToDTO(Handleliste handleliste)
        {
            var varer = handleliste.Varer.Select(v => new VareInstanceDTO
            {
                VareId = v.VareId,
                VareName = v.Vare.VareName,
                Mengde = v.Mengde,
                IsCollected = v.IsCollected,
            }).ToList();
            return new HandlelisteWithVarerDTO
            {
                HandlelisteId = handleliste.HandlelisteId,
                UserId = handleliste.UserId,
                HandlelisteName = handleliste.HandlelisteName,
                Varer = varer
            };
        }

        public VareInstance VareInstanceFromDTO(int handlelisteId, VareInstanceDTO vareInstanceDTO)
        {
            return new VareInstance()
            {
                HandlelisteId = handlelisteId,
                VareId = vareInstanceDTO.VareId,
                Mengde = vareInstanceDTO.Mengde,
                IsCollected = vareInstanceDTO.IsCollected,
            };
        }

        public VareInstanceDTO VareInstanceToDTONoName(VareInstance vareInstance)
        {
            return new VareInstanceDTO()
            {
                VareId = vareInstance.VareId,
                Mengde = vareInstance.Mengde,
                IsCollected = vareInstance.IsCollected,
            };
        }

        private static VareInstanceDTO VareInstanceToDTO(VareInstance vareInstance, string vareName)
        {
            return new VareInstanceDTO
            {
                VareId = vareInstance.VareId,
                VareName = vareName,
                Mengde = vareInstance.Mengde,
                IsCollected = vareInstance.IsCollected,
            };
        }
    }
}
