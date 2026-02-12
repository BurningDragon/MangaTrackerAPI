using MangaTracker.Dtos;
using MangaTracker.Models;
using MangaTracker.Repository.Interfaces;
using MangaTracker.Services.Interfaces;

namespace MangaTracker.Services
{
    public class MangaService(IMangaRepository repository) : IMangaService
    {
        private readonly IMangaRepository _repository = repository;
        public async Task<OperationResult> AddAsync(Manga entity)
        {
            var alreadyExistsMessage = "Manga already exists";

            if (await _repository.ExistsAsync(entity.Id))
            {
                return new OperationResult { Success = false, Errors = [alreadyExistsMessage] };
            }
            else if (await _repository.ExistsAsync(entity.Title, entity.Creators, entity.Volume))
            {
                return new OperationResult { Success = false, Errors = [alreadyExistsMessage] };
            }

            await _repository.AddMangaAsync(entity);
            await _repository.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<OperationResult> DeleteMangaAsync(int id)
        {
            var manga = await _repository.GetByIdAsync(id);
            if (manga == null)
            {
                return new OperationResult { Success = false, Errors = [$"Could not find manga with Id {id}"] };
            }

            _repository.DeleteManga(manga);
            await _repository.SaveChangesAsync();
            return new OperationResult { Success = true };
        }

        public async Task<Manga?> GetMangaByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Manga>> GetMangasAsync()
        {
            return await _repository.GetAllMangasAsync();
        }

        public async Task<OperationResult> UpdateManga(MangaForCreatorDto entity)
        {
            var notExistsMessage = "Manga does not exist";

            if (!await _repository.ExistsAsync(entity.Id))
            {
                return new OperationResult { Success = false, Errors = [notExistsMessage] };
            }

            var manga = await _repository.GetByIdAsync(entity.Id);
            if (manga == null)
            {
                return new OperationResult { Success = false, Errors = [$"Manga with Id {entity.Id} could not be found"] };
            }

            manga.Description = entity.Description;
            manga.Title = entity.Title;
            manga.Volume = entity.Volume;
            manga.ReleaseDate = entity.ReleaseDate;
            manga.IsLightNovel = entity.IsLightNovel;

            _repository.UpdateManga(manga);
            await _repository.SaveChangesAsync();
            return new OperationResult { Success = true };
        }
    }
}
