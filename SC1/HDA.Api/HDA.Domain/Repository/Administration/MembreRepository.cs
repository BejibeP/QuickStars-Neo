using HDA.Domain.Entities;
using HDA.Domain.Interfaces;

namespace HDA.Domain.Repositories
{
    public class MembreRepository : BaseRepository<Membre>, IMembreRepository
    {
        public MembreRepository(MyDbContext databaseContext) : base(databaseContext)
        {

        }
    }
}