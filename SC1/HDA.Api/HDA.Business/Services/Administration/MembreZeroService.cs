using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Interfaces;

namespace HDA.Business.Services
{
    public class MembreZeroService : IMembreZeroService
    {
        private readonly IMembreZeroRepository _repository;
        public MembreZeroService(IMembreZeroRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MembreZeroDto>> GetAllMembres()
        {
            var items = await _repository.GetAll();
            return items.Select(x => new MembreZeroDto()
            {
                Id = x.Id,
                Batiment = x.Batiment,
                Nom = x.Nom,
                Prenom = x.Prenom,
                Tel = x.Tel,
                Email = x.Email,
                Active = x.Active
            });
        }

        public async Task<MembreZeroDto> GetMembreById(long id)
        {
            var dbItem = await _repository.GetById(id);

            if (dbItem == null) return new MembreZeroDto();

            var membreDto = new MembreZeroDto()
            {
                Id = dbItem.Id,
                Batiment = dbItem.Batiment,
                Nom = dbItem.Nom,
                Prenom = dbItem.Prenom,
                Tel = dbItem.Tel,
                Email = dbItem.Email,
                Active = dbItem.Active
            };
            return membreDto;
        }

    }
}