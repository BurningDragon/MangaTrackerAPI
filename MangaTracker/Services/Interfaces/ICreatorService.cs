using MangaTracker.Dtos;
using MangaTracker.Models;

namespace MangaTracker.Services.Interfaces
{
    public interface ICreatorService
    {
        Task<OperationResult> AddAsync(Creator entity);
        Task<IEnumerable<Creator>> GetCreatorsAsync();
        Task<Creator?> GetCreatorByIdAsync(int id);
    }
}
