using MangaTracker.Dtos;
using MangaTracker.Models;

namespace MangaTracker.Services.Interfaces
{
    public interface IMangaService
    {
        Task<OperationResult> AddAsync(Manga entity);
        Task<IEnumerable<Manga>> GetMangasAsync();
        Task<Manga?> GetMangaByIdAsync(int id);
        Task<OperationResult> UpdateManga(MangaForCreatorDto entity);
        Task<OperationResult> DeleteMangaAsync(int id);
    }
}
