using HDA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Interfaces
{
    public interface IFormuleRepasRepository
    {
        Task<IEnumerable<FormuleRepas>> GetAllFormulesRepas();
        Task<IEnumerable<FormuleRepas>> GetAllAvailableRepas();
        Task<FormuleRepas?> GetFormuleRepasById(int id);
        Task<int> CreateFormuleRepas(FormuleRepas formuleRepas);
        Task<bool> DeleteFormuleRepas(int id);
        Task<bool> UpdateFormuleRepas(FormuleRepas formuleRepas);
    }
}
