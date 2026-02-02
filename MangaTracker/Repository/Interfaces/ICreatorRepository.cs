using MangaTracker.Models;
using System.Collections;

namespace MangaTracker.Repository.Interfaces
{
    public interface ICreatorRepository
    {
        Task<IEnumerable<Creator>> GetAllCreatorsAsync();
        Task<Creator?> GetByIdAsync(int Id);
        Task AddCreatorAsync(Creator creator);
        void UpdateCreator(Creator creator);
        void DeleteCreator(Creator creator);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(int Id);
        Task<bool>ExistsAsync(string firstname, string lastname);
    }
}
