using HDA.Business.Models;

namespace HDA.Business.Interfaces
{
    public interface IResasZeroService
    {
        Task<IEnumerable<ResasZeroDto>> GetAllReservations();
        Task<ResasZeroDto> GetReservationById(long id);
    }
}