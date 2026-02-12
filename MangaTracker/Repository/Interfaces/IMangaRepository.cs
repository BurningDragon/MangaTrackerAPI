using MangaTracker.Models;

namespace MangaTracker.Repository.Interfaces
{
    public interface IMangaRepository
    {
        Task<IEnumerable<Manga>> GetAllMangasAsync();
        Task<Manga?> GetByIdAsync(int Id);
        Task AddMangaAsync(Manga manga);
        void UpdateManga(Manga manga);
        void DeleteManga(Manga manga);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(int Id);
        Task<bool> ExistsAsync(string title, IEnumerable<Creator> creators, int volume);
    }
}
