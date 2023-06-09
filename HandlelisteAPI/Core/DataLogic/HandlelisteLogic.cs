﻿using HandlelisteAPI.Core.DTO;
using HandlelisteData;
using HandlelisteDomain;
using Microsoft.EntityFrameworkCore;

namespace HandlelisteAPI.Core.DataLogic
{
    public class HandlelisteLogic : DataLogic
    {

        public HandlelisteLogic(HandlelisteContext context)
            : base(context)
        {
        }

        public async Task<Handleliste?> GetHandlelisteById(int id)
        {
            var handleliste = await _context.Handlelister.FindAsync(id);
            if (handleliste == null)
                return null;
            return handleliste;
        }

        public async Task<List<HandlelisteDTO>> GetHandlelisterByUserIdDTO(string userId)
        {
            var handlelister = await _context.Handlelister
                .Where(h => h.UserId == userId)
                .Select(a => HandlelisteToDTO(a))
                .ToListAsync();

            return handlelister;
        }

        public bool HandlelisteExists(int id)
        {
            return (_context.Handlelister?.Any(h => h.HandlelisteId == id)).GetValueOrDefault();
        }

        public void SetModifiedHandlelisteState(Handleliste handleliste)
        {
            _context.Entry(handleliste).State = EntityState.Modified;
        }

        public void AddHandleliste(Handleliste handleliste)
        {
            _context.Handlelister.Add(handleliste);
        }

        public void RemoveHandleliste(Handleliste handleliste)
        {
            _context.Handlelister.Remove(handleliste);
        }

        public HandlelisteDTO HandlelisteToDTO(Handleliste handleliste)
        {
            return new HandlelisteDTO
            {
                HandlelisteId = handleliste.HandlelisteId,
                UserId = handleliste.UserId,
                HandlelisteName = handleliste.HandlelisteName,
            };
        }
    }
}
