using HDA.Business.Models;

namespace HDA.Business.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservations();
        Task<IEnumerable<ReservationPublicDto>> GetAllReservationsPubliques();
        Task<ReservationDto> GetReservationById(long id);
        Task<ReservationDto> AddReservation(ReservationDto dto);
        Task<ReservationDto> UpdateReservation(ReservationDto dto);
        Task<bool> DeleteReservation(long id);
    }
}