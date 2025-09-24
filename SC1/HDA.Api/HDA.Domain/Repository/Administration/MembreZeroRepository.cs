using HDA.Domain.Entities;
using HDA.Domain.Interfaces;

namespace HDA.Domain.Repositories
{
    public class MembreZeroRepository : BaseRepository<MembreZero>, IMembreZeroRepository
    {
        public MembreZeroRepository(MyDbContext databaseContext) : base(databaseContext)
        {

        }
    }
}