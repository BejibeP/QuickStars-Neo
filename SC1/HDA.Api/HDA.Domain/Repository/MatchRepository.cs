using HDA.Domain.Entities;
using HDA.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HDA.Domain.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly MyDbContext _dbContext;
        public MatchRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retourne le match ouvert, ou en assigne un (au dernier créé) s'il n'y en a pas
        /// </summary>
        public async Task<MatchSoccer?> GetOpenMatch()
        {
            var sortie = await _dbContext.MatchsSoccer.SingleOrDefaultAsync(m => m.InscriptionsOuvertes == true);
            return sortie;
        }

        public async Task<MatchSoccer?> GetMatchById(int id)
        {
            var sortie = await _dbContext.MatchsSoccer.SingleOrDefaultAsync(m => m.Id == id);
            return sortie;
        }

        public async Task<MatchSoccer> GetLastMatch()
        {
            var id = await _dbContext.MatchsSoccer.MaxAsync(m => m.Id);
            var match = await _dbContext.MatchsSoccer.FirstAsync(m => m.Id == id);
            return match;
        }

        public async Task<IEnumerable<MatchSoccer>> GetAllMatch()
        {
            var sortie = await _dbContext.MatchsSoccer.Select(m => m).ToListAsync();
            return sortie;
        }

        public async Task<IEnumerable<MatchSoccer>> GetAllClosedMatch()
        {
            var sortie = await _dbContext.MatchsSoccer.Where(m => m.InscriptionsOuvertes == false).ToListAsync();
            return sortie;
        }

        public async Task CloseMatch(MatchSoccer matchAFermer)
        {
            matchAFermer.InscriptionsOuvertes = false;
            _dbContext.MatchsSoccer.Update(matchAFermer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task OpenMatch(MatchSoccer matchAOuvrir)
        {
            matchAOuvrir.InscriptionsOuvertes = true;
            _dbContext.MatchsSoccer.Update(matchAOuvrir);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateMatch(MatchSoccer match)
        {
            var sortie = await _dbContext.AddAsync(match);
            await _dbContext.SaveChangesAsync();
            return sortie.Entity.Id;
        }

        public async Task<bool> DeleteMatch(int id)
        {
            if (id == 0) return false;
            var match = await GetMatchById(id);
            if (match is null) return false;

            var sortie = _dbContext.MatchsSoccer.Remove(match);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
