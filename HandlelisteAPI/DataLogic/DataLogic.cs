using HandlelisteData;

namespace HandlelisteAPI.DataLogic
{
    public class DataLogic
    {
        public readonly HandlelisteContext _context;
        public DataLogic(HandlelisteContext context)
        {
            _context = context;
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
