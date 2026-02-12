using MangaTracker.Models;
using MangaTracker.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MangaTracker.Repository.Repositories
{
    public class CreatorRepository(MangaTrackerDbContext dbContext) : ICreatorRepository, IDisposable
    {
        private readonly MangaTrackerDbContext _dbContext = dbContext;
        private bool _disposed;

        public async Task AddCreatorAsync(Creator creator)
        {
            await _dbContext.AddAsync(creator);
        }

        public void DeleteCreator(Creator creator)
        {
            _dbContext.Remove(creator);
        }

        public async Task<IEnumerable<Creator>> GetAllCreatorsAsync()
        {
            return await _dbContext.Creators.Include(c => c.Mangas).ToListAsync();
        }

        public async Task<Creator?> GetByIdAsync(int Id)
        {
            return await _dbContext.Creators.Include(c => c.Mangas!).ThenInclude(m => m.Creators).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public void UpdateCreator(Creator creator)
        {
            _dbContext.Creators.Update(creator);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
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

        public async Task<bool> ExistsAsync(int Id)
        {
            return await _dbContext.Creators.AnyAsync(c => c.Id == Id);
        }

        public async Task<bool> ExistsAsync(string firstname, string lastname)
        {
            return await _dbContext.Creators.AnyAsync(c => c.FirstName.Equals(firstname) && c.LastName.Equals(lastname));
        }
    }
}
