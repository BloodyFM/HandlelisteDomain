using HandlelisteData;

namespace HandlelisteAPI.Core.DataLogic
{
    public class DataLogic
    {
        public readonly HandlelisteContext _context;
        public DataLogic(HandlelisteContext context)
        {
            _context = context;
        }

        public bool VareInstanceExists(int handlelisteId, int vareId)
        {
            return (_context.Vareinstance?.Any(vi => vi.HandlelisteId == handlelisteId && vi.VareId == vareId)).GetValueOrDefault();
        }

        public bool HandlelisteExists(int id)
        {
            return (_context.Handlelister?.Any(h => h.HandlelisteId == id)).GetValueOrDefault();
        }

        public bool VareExists(int id)
        {
            return (_context.Varer?.Any(v => v.VareId == id)).GetValueOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool VarerIsNull()
        {
            if (_context.Varer == null) return true;
            else return false;
        }

        public bool VareInstanceIsNull()
        {
            if (_context.Vareinstance == null) return true;
            else return false;
        }

        public bool HandlelisterIsNull()
        {
            if (_context.Handlelister == null) return true;
            else return false;
        }
    }
}
