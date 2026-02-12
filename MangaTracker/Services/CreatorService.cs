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
            var alreadyExistsMessage = "Creator already exists";

            if (await _repository.ExistsAsync(entity.Id))
            {
                return new OperationResult { Success = false, Errors = [alreadyExistsMessage] };
            }
            else if (await _repository.ExistsAsync(entity.FirstName, entity.LastName))
            {
                return new OperationResult { Success = false, Errors = [alreadyExistsMessage] };
            }

            await _repository.AddCreatorAsync(entity);
            await _repository.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<OperationResult> DeleteCreatorAsync(int id)
        {
            var creator = await _repository.GetByIdAsync(id);
            if (creator == null)
            {
                return new OperationResult { Success = false, Errors = [$"Could not find creator with Id {id}"] };
            }

            _repository.DeleteCreator(creator);
            await _repository.SaveChangesAsync();
            return new OperationResult { Success = true };

        }

        public async Task<IEnumerable<Creator>> GetCreatorsAsync()
        {
            return await _repository.GetAllCreatorsAsync();
        }

        public async Task<OperationResult> UpdateCreator(CreatorForMangaDto entity)
        {
            var notExistsMessage = "Creator does not exist";

            if (!await _repository.ExistsAsync(entity.Id))
            {
                return new OperationResult { Success = false, Errors = [notExistsMessage] };
            }

            var creator = await _repository.GetByIdAsync(entity.Id);
            if(creator == null)
            {
                return new OperationResult { Success = false, Errors = [$"Creator with Id {entity.Id} could not be found"] };
            }

            creator.FirstName = entity.FirstName;
            creator.LastName = entity.LastName;
            creator.IsAuthor = entity.IsAuthor;
            creator.IsIllustrator = entity.IsIllustrator;

            _repository.UpdateCreator(creator);
            await _repository.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<Creator?> GetCreatorByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
