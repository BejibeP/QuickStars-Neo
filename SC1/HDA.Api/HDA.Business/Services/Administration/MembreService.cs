using HDA.Business.Interfaces;
using HDA.Business.Models;
using HDA.Domain.Entities;
using HDA.Domain.Interfaces;

namespace HDA.Business.Services
{
    public class MembreService : IMembreService
    {

        private readonly IMembreRepository _repository;
        public MembreService(IMembreRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MembreDto>> GetAllMembres()
        {
            var dbItems = await _repository.GetAll();
            return dbItems.Select(x => new MembreDto()
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

        public async Task<MembreDto> GetMembreById(long id)
        {
            var dbItem = await _repository.GetById(id);

            if (dbItem == null) return new MembreDto();

            var membreDto = new MembreDto()
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

        public async Task<MembreDto> AddMembre(MembreDto dto)
        {
            var membre = new Membre() 
            {
                Batiment = dto.Batiment, 
                Nom = dto.Nom, 
                Prenom = dto.Prenom, 
                Tel = dto.Tel, 
                Email = dto.Email, 
                Active = dto.Active 
            };
            var item = await _repository.Create(membre);

            if(item == null) return new MembreDto();

            return new MembreDto()
            {
                Id = item.Id,
                Batiment = item.Batiment,
                Nom = item.Nom,
                Prenom = item.Prenom,
                Tel = item.Tel,
                Email = item.Email,
                Active = item.Active
            };
        }

        public async Task<MembreDto> UpdateMembre(MembreDto dto)
        {
            var membre = new Membre()
            {
                Batiment = dto.Batiment,
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                Tel = dto.Tel,
                Email = dto.Email,
                Active = dto.Active
            };
            var item = await _repository.Update(membre);

            if (item == null) return new MembreDto();

            return new MembreDto()
            {
                Id = item.Id,
                Batiment = item.Batiment,
                Nom = item.Nom,
                Prenom = item.Prenom,
                Tel = item.Tel,
                Email = item.Email,
                Active = item.Active
            };
        }

        public async Task<bool> DeleteMembre(long id)
        {
            return await _repository.Delete(id);
        }

    }
}