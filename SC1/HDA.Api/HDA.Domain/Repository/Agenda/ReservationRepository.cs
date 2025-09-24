using HDA.Domain.Entities;
using HDA.Domain.Interfaces;

namespace HDA.Domain.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(MyDbContext databaseContext) : base(databaseContext)
        {

        }
    }
}