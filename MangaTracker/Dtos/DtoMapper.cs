using MangaTracker.Models;

namespace MangaTracker.Dtos
{
    public static class DtoMapper
    {
        public static CreatorDto MapToCreatorDto(Creator creator)
        {
            var mangas = new List<MangaDto>();
            if (creator.Mangas != null && creator.Mangas.Count > 0)
            {
                foreach (var m in creator.Mangas)
                {
                    mangas.Add(MapToMangaForCreatorDto(creator.Id, m));
                }
            }

            return new CreatorDto
            {
                Id = creator.Id,
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                IsAuthor = creator.IsAuthor,
                IsIllustrator = creator.IsIllustrator,
                Mangas = mangas
            };
        }

        public static MangaDto MapToMangaForCreatorDto(int creatorId, Manga manga)
        {
            var creatorsForMangaDtos = new List<CreatorForMangaDto>();
            if (manga.Creators.Count > 1)
            {
                foreach(var c in manga.Creators.Where(c => c.Id != creatorId))
                {
                    creatorsForMangaDtos.Add(MapToCreatorForMangaDto(c));
                }
            }

            return new MangaDto
            {
                Id = manga.Id,
                Description = manga.Description,
                ReleaseDate = manga.ReleaseDate,
                Title = manga.Title,
                IsLightNovel = manga.IsLightNovel,
                Volume = manga.Volume,
                Creators = creatorsForMangaDtos
            };
        }

        public static CreatorForMangaDto MapToCreatorForMangaDto(Creator creator)
        {
            return new CreatorForMangaDto
            {
                Id = creator.Id,
                FirstName= creator.FirstName,
                LastName= creator.LastName,
                IsAuthor = creator.IsAuthor,
                IsIllustrator= creator.IsIllustrator
            };
        }

        public static MangaDto MapToMangaDto(Manga manga)
        {
            var creators = new List<CreatorForMangaDto>();

            foreach (var creator in manga.Creators)
            {
                creators.Add(MapToCreatorForMangaDto(creator));
            }

            return new MangaDto
            {
                Id = manga.Id,
                Title = manga.Title,
                Description = manga.Description,
                ReleaseDate = manga.ReleaseDate,
                Volume = manga.Volume,
                IsLightNovel = manga.IsLightNovel,
                Creators = creators
            };
        }
    }
}
