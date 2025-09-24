using HDA.Domain.Entities;
using HDA.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HDA.Domain.Repository
{
    public class JoueurRepository : IJoueurRepository
    {
        private readonly MyDbContext _dbContext;
        public JoueurRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Joueur>> GetAllJoueurs()
        {
            var list = await _dbContext.Joueurs.ToListAsync();
            return list;
        }
        public async Task<int> GetJoueurByMail(string email)
        {
            var sortie = await _dbContext.Joueurs.SingleOrDefaultAsync(j => j.Mail == email);
            if (sortie == null) return 0;
            return sortie.Id;
        }
        public async Task<Joueur?> GetJoueurById(int id)
        {
            return await _dbContext.Joueurs.FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task<int> AddJoueur(Joueur joueur)
        {
            var entry = await _dbContext.Joueurs.AddAsync(joueur);
            await _dbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task<bool> UpdateJoueur(Joueur joueur)
        {
            if (joueur == null) return false;
            _dbContext.Joueurs.Update(joueur);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteJoueur(int id)
        {
            var joueur = await GetJoueurById(id);
            if (joueur is null) return false;

            _dbContext.Joueurs.Remove(joueur);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
