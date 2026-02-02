using MangaTracker.Models;
using MangaTracker.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MangaTracker.Repository.Repositories
{
    public class MangaRepository(MangaTrackerDbContext dbContext) : IMangaRepository, IDisposable
    {
        private readonly MangaTrackerDbContext _dbContext = dbContext;
        private bool _disposed;
        void IMangaRepository.DeleteManga(Manga manga)
        {
            _dbContext.Remove(manga);
        }

        async Task<bool> IMangaRepository.ExistsAsync(int Id)
        {
            return await _dbContext.Mangas.AnyAsync(c => c.Id == Id);
        }

        async Task<bool> IMangaRepository.ExistsAsync(string title, int creatorId)
        {
            return await _dbContext.Mangas.AnyAsync(c => c.Title.Equals(title) && c.Creators.Any(c => c.Id == creatorId));
        }

        async Task<IEnumerable<Manga>> IMangaRepository.GetAllMangasAsync()
        {
            return await _dbContext.Mangas.Include(m => m.Creators).ToListAsync();
        }

        async Task<Manga?> IMangaRepository.GetByIdAsync(int Id)
        {
            return await _dbContext.Mangas.Include(m => m.Creators).FirstOrDefaultAsync(m => m.Id == Id);
        }

        async Task IMangaRepository.SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        void IMangaRepository.UpdateManga(Manga manga)
        {
            _dbContext.Mangas.Update(manga);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
