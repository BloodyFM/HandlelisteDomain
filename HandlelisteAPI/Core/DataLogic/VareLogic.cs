using HandlelisteAPI.Core.DTO;
using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;

namespace HandlelisteAPI.Core.DataLogic
{
    public class VareLogic : DataLogic
    {
        public VareLogic(HandlelisteContext context)
            : base(context)
        {
        }

        public async Task<List<VareDTO>> GetAllVarerDTO()
        {
            var vareList = await _context.Varer.ToListAsync();
            var vareDTOList = new List<VareDTO>();
            foreach (var vare in vareList)
            {
                vareDTOList.Add(VareToDTO(vare));
            }
            return vareDTOList;
        }

        public async Task<VareDTO?> GetVareByIdDTO(int vareId)
        {
            var vare = await _context.Varer.FindAsync(vareId);
            if (vare == null)
                return null;
            return VareToDTO(vare);
        }

        public async Task<Handleliste?> GetHandlelisteIncludingVarerById(int handlelisteId)
        {
            return await _context.Handlelister.Include(h => h.Varer).FirstOrDefaultAsync(h => h.HandlelisteId == handlelisteId);
        }

        public async Task<List<Handleliste>> GetUsersHandlelisterThatContainsVareById(string userId, int vareId)
        {
            return await _context.Handlelister
                    .Include(h => h.Varer).ThenInclude(v => v.Vare)
                    .Where(h => h.UserId == userId && h.Varer.Any(v => v.VareId == vareId))
                    .ToListAsync();
        }

        public void AddNewVare(Vare newVare)
        {
            _context.Varer.Add(newVare);
        }

        public bool VareExists(int id)
        {
            return (_context.Varer?.Any(v => v.VareId == id)).GetValueOrDefault();
        }

        public Vare VareFromDTO(VareDTO vareDTO)
        {
            return new Vare { VareId = vareDTO.VareId, VareName = vareDTO.VareName, VareInstancer = new List<VareInstance>() };
        }
        public VareDTO VareToDTO(Vare vare)
        {
            return new VareDTO { VareId = vare.VareId, VareName = vare.VareName };
        }
    }
}
