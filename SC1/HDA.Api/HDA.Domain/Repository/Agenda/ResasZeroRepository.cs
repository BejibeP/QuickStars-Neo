using HDA.Domain.Entities;
using HDA.Domain.Interfaces;

namespace HDA.Domain.Repositories
{
    public class ResasZeroRepository : BaseRepository<ResasZero>, IResasZeroRepository
    {
        public ResasZeroRepository(MyDbContext databaseContext) : base(databaseContext)
        {

        }
    }
}