using HDA.Business.Models;

namespace HDA.Business.Interfaces
{
    public interface IMembreZeroService
    {
        Task<IEnumerable<MembreZeroDto>> GetAllMembres();
        Task<MembreZeroDto> GetMembreById(long id);
    }
}