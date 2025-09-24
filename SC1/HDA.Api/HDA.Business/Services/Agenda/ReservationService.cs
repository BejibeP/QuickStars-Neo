using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Entities;
using HDA.Domain.Interfaces;

namespace HDA.Business.Services
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository _repository;
        public ReservationService(IReservationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservations()
        {
            var dbItems = await _repository.GetAll();
            return dbItems.Select(x => new ReservationDto()
            {
                Id = x.Id,
                DateDebut = x.DateDebut,
                DateFin = x.DateFin,
                Nom = x.Nom,
                Prenom = x.Prenom,
                Motif = x.Motif,
                Prive = x.Prive
            });
        }

        public async Task<IEnumerable<ReservationPublicDto>> GetAllReservationsPubliques()
        {
            var dbItems = await _repository.GetAll();
            return dbItems.Select(x => new ReservationPublicDto()
            {
                Id = x.Id,
                DateDebut = x.DateDebut,
                DateFin = x.DateFin
            });
        }

        public async Task<ReservationDto> GetReservationById(long id)
        {
            var dbItem = await _repository.GetById(id);

            if (dbItem == null) return new ReservationDto();

            var membreDto = new ReservationDto()
            {
                Id = dbItem.Id,
                DateDebut = dbItem.DateDebut,
                DateFin = dbItem.DateFin,
                Nom = dbItem.Nom,
                Prenom= dbItem.Prenom,
                Motif= dbItem.Motif,
                Prive= dbItem.Prive
            };
            return membreDto;
        }

        public async Task<ReservationDto> AddReservation(ReservationDto dto)
        {
            var membre = new Reservation()
            {
                DateDebut= dto.DateDebut,
                DateFin= dto.DateFin,
                Nom = dto.Nom,
                Prenom= dto.Prenom,
                Motif= dto.Motif,
                Prive= dto.Prive
            };
            var item = await _repository.Create(membre);

            if (item == null) return new ReservationDto();

            return new ReservationDto()
            {
                Id= item.Id,
                DateDebut= dto.DateDebut,
                DateFin= dto.DateFin,
                Nom= dto.Nom,
                Prenom= dto.Prenom,
                Motif= dto.Motif,
                Prive= dto.Prive
            };
        }

        public async Task<ReservationDto> UpdateReservation(ReservationDto dto)
        {
            var membre = new Reservation()
            {
                Id = dto.Id,
                DateDebut = dto.DateDebut,
                DateFin = dto.DateFin,
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                Motif = dto.Motif,
                Prive = dto.Prive
            };
            var item = await _repository.Update(membre);

            if (item == null) return new ReservationDto();

            return new ReservationDto()
            {
                Id = item.Id,
                DateDebut = dto.DateDebut,
                DateFin = dto.DateFin,
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                Motif = dto.Motif,
                Prive = dto.Prive
            };
        }

        public async Task<bool> DeleteReservation(long id)
        {
            return await _repository.Delete(id);
        }

    }
}