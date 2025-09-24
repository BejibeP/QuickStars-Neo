using HDA.Business.Models.FormuleRepas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Business.Interfaces
{
    public interface IFormuleRepasService
    {
        Task<IEnumerable<FormuleRepasDTO>> GetAllFormulesRepas();
        Task<IEnumerable<FormuleRepasDTO>> GetAllAvailableRepas();
        Task<FormuleRepasDTO?> CreateFormuleRepas(CreateFormuleRepasDTO formuleRepas);
        Task<bool> DeleteFormuleRepas(int id);
        Task<bool> UpdateFormuleRepas(FormuleRepasDTO dto, int id);
    }
}
