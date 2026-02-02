using MangaTracker.Dtos;
using MangaTracker.Models;
using MangaTracker.Repository.Interfaces;
using MangaTracker.Services.Interfaces;

namespace MangaTracker.Services
{
    public class CreatorService(ICreatorRepository repository) : ICreatorService
    {
        private readonly ICreatorRepository _repository = repository;

        public async Task<OperationResult> AddAsync(Creator entity)
        {
            if (await _repository.ExistsAsync(entity.Id))
            {
                return new OperationResult { Success = false, Errors = ["Creator already exists"] };
            }
            else if (await _repository.ExistsAsync(entity.FirstName, entity.LastName))
            {
                return new OperationResult { Success = false, Errors = ["Creator already exists"] };
            }

            await _repository.AddCreatorAsync(entity);
            await _repository.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<IEnumerable<Creator>> GetCreatorsAsync()
        {
            return await _repository.GetAllCreatorsAsync();
        }

        async Task<Creator?> ICreatorService.GetCreatorByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
