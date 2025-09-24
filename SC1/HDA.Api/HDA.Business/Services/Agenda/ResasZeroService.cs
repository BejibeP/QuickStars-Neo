using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Interfaces;

namespace HDA.Business.Services
{
    public class ResasZeroService : IResasZeroService
    {

        private readonly IResasZeroRepository _repository;
        public ResasZeroService(IResasZeroRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ResasZeroDto>> GetAllReservations()
        {
            var items = await _repository.GetAll();
            return items.Select(x => new ResasZeroDto()
            {
                Id = x.Id,
                DateDeb = x.DateDeb,
                DateFin = x.DateFin,
                HeureDeb = x.HeureDeb,
                HeureFin = x.HeureFin,
                Motif = x.Motif,
                Nom = x.Nom,
                Prenom = x.Prenom,
                Prive = x.Prive
            });
        }

        public async Task<ResasZeroDto> GetReservationById(long id)
        {
            var dbItem = await _repository.GetById(id);

            if (dbItem == null) return new ResasZeroDto();

            var membreDto = new ResasZeroDto()
            {
                Id = dbItem.Id,
                DateDeb = dbItem.DateDeb,
                DateFin = dbItem.DateFin,
                HeureDeb = dbItem.HeureDeb,
                HeureFin = dbItem.HeureFin,
                Motif = dbItem.Motif,
                Nom = dbItem.Nom,
                Prenom = dbItem.Prenom,
                Prive = dbItem.Prive
            };
            return membreDto;
        }

    }
}