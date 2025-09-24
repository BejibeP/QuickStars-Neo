using HDA.Domain.Entities;
using HDA.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HDA.Domain.Repository
{
    public class JoueurMatchRepository : IJoueurMatchRepository
    {
        private readonly MyDbContext _dbContext;
        public JoueurMatchRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddJoueurToMatch(JoueursMatch joueurMatch)
        {
            var sortie = await _dbContext.AddAsync(joueurMatch);
            await _dbContext.SaveChangesAsync();
            return sortie.Entity.Id;
        }

        public async Task<bool> RemoveJoueurToMatch(int idMatch, int idJoueur)
        {
            var joueur = await _dbContext.JoueursMatchs.Where(jm => jm.MatchSoccerId == idMatch).SingleOrDefaultAsync(jm => jm.JoueurId == idJoueur);
            if (joueur == null) return false;
            _dbContext.JoueursMatchs.Remove(joueur);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRepasOfJoueurMatch(JoueursMatch joueurMatch)
        {
            var joueur = await _dbContext.JoueursMatchs.FirstOrDefaultAsync(jm => jm.JoueurId == joueurMatch.Id);
            if (joueur == null) { return false; }
            joueur.FormuleRepas = joueurMatch.FormuleRepas;
            _dbContext.JoueursMatchs.Update(joueur);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<ICollection<JoueursMatch>> GetJoueursFromMatch(int id)
        {
            var list = await _dbContext.JoueursMatchs.Where(jm => jm.MatchSoccerId == id).Include(jm => jm.Joueur).ToListAsync();
            return list;
        }
    }
}
