using HDA.Business.Models;

namespace HDA.Business.Interfaces
{
    public interface IMembreService
    {
        Task<IEnumerable<MembreDto>> GetAllMembres();
        Task<MembreDto> GetMembreById(long id);
        Task<MembreDto> AddMembre(MembreDto dto);
        Task<MembreDto> UpdateMembre(MembreDto dto);
        Task<bool> DeleteMembre(long id);
    }
}