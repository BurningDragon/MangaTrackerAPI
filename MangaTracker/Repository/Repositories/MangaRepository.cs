using MangaTracker.Models;
using MangaTracker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MangaTracker.Repository.Repositories
{
    public class MangaRepository(MangaTrackerDbContext dbContext) : IMangaRepository, IDisposable
    {
        private readonly MangaTrackerDbContext _dbContext = dbContext;
        private bool _disposed;

        public async Task AddMangaAsync(Manga manga)
        {
            await _dbContext.AddAsync(manga);
        }

        void IMangaRepository.DeleteManga(Manga manga)
        {
            _dbContext.Remove(manga);
        }

        async Task<bool> IMangaRepository.ExistsAsync(int Id)
        {
            return await _dbContext.Mangas.AnyAsync(c => c.Id == Id);
        }

        async Task<bool> IMangaRepository.ExistsAsync(string title,IEnumerable<Creator> creators, int volume)
        {
            var result = false;

            if (await _dbContext.Mangas.AnyAsync())
            {
                var mangas = await _dbContext.Mangas.Where(m => m.Title  == title && m.Volume == volume).ToListAsync();
                if(mangas.Count == 0)
                {
                    return result;
                }

                foreach (var manga in mangas)
                {
                    result = manga.Creators.All(c => creators.All(cr => cr.Id == c.Id));
                    if (result)
                    {
                        return result;
                    }
                }
            }
            return result;
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
