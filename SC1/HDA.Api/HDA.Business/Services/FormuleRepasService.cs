using HDA.Business.Enumeration;
using HDA.Business.Interfaces;
using HDA.Business.Models.FormuleRepas;
using HDA.Domain.Entities;
using HDA.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Services
{
    public class FormuleRepasService : IFormuleRepasService
    {
        private readonly IFormuleRepasRepository _formuleRepasRepository;
        public FormuleRepasService(IFormuleRepasRepository formuleRepasRepository)
        {
            _formuleRepasRepository=formuleRepasRepository;
        }

        public async Task<IEnumerable<FormuleRepasDTO>> GetAllFormulesRepas()
        {
            var list = await _formuleRepasRepository.GetAllFormulesRepas();
            var sortie = list.Select(fr => 
                new FormuleRepasDTO() { 
                    EstDisponible = fr.EstDisponible,
                    Id = fr.Id,
                    Nom = fr.Nom,
                }).ToList();
            return sortie;
        }

        public async Task<IEnumerable<FormuleRepasDTO>> GetAllAvailableRepas()
        {
            var list = await _formuleRepasRepository.GetAllFormulesRepas();
            var sortie = list.Where(fr => fr.EstDisponible == true).Select(fr =>
                new FormuleRepasDTO()
                {
                    EstDisponible = fr.EstDisponible,
                    Id = fr.Id,
                    Nom = fr.Nom,
                }).ToList();
            return sortie;
        }
        public async Task<FormuleRepasDTO?> CreateFormuleRepas(CreateFormuleRepasDTO formuleRepas)
        {
            if (string.IsNullOrEmpty(formuleRepas.Nom)) return null;
            var fo = new FormuleRepas() { Nom = formuleRepas.Nom };
            int id = await _formuleRepasRepository.CreateFormuleRepas(fo);
            return new FormuleRepasDTO() { EstDisponible = true, Id = id, Nom = formuleRepas.Nom };
        }

        public async Task<bool> DeleteFormuleRepas(int id)
        {
            return await _formuleRepasRepository.DeleteFormuleRepas(id);
        }

        public async Task<bool> UpdateFormuleRepas(FormuleRepasDTO dto, int id)
        {
            var fo = await _formuleRepasRepository.GetFormuleRepasById(id);
            if (fo == null) return false;
            if (string.IsNullOrEmpty(dto.Nom)) return false;

            fo.EstDisponible = dto.EstDisponible;
            fo.Nom = dto.Nom;

            return await _formuleRepasRepository.UpdateFormuleRepas(fo);
        }
    }
}
