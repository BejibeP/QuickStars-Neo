using HDA.Domain.Entities;
using HDA.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HDA.Domain.Repository
{
    public class FormuleRepasRepository : IFormuleRepasRepository
    {
        private readonly MyDbContext _context;
        public FormuleRepasRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FormuleRepas>> GetAllFormulesRepas()
        {
            var sortie = await _context.FormulesRepas.ToListAsync();
            return sortie;
        }

        public async Task<IEnumerable<FormuleRepas>> GetAllAvailableRepas()
        {
            var sortie = await _context.FormulesRepas.Where(fr => fr.EstDisponible == true).ToListAsync();
            return sortie;
        }

        public async Task<FormuleRepas?> GetFormuleRepasById(int id)
        {
            if (id == 0) return null;
            return await _context.FormulesRepas.FirstOrDefaultAsync(fr => fr.Id == id);
        }
        public async Task<int> CreateFormuleRepas(FormuleRepas formuleRepas)
        {
            var sortie = await _context.FormulesRepas.AddAsync(formuleRepas);
            await _context.SaveChangesAsync();
            return sortie.Entity.Id;
        }

        public async Task<bool> DeleteFormuleRepas(int id)
        {
            if (id == 0) return false;
            var formule = await GetFormuleRepasById(id);
            if (formule is null) return false;

            var sortie = _context.FormulesRepas.Remove(formule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateFormuleRepas(FormuleRepas formuleRepas)
        {
            var formule = await GetFormuleRepasById(formuleRepas.Id);
            if (formule is null) return false;
            _context.FormulesRepas.Update(formuleRepas);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
