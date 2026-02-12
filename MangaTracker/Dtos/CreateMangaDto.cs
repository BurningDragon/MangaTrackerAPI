using MangaTracker.Models;

namespace MangaTracker.Dtos
{
    public class CreateMangaDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public bool IsLightNovel { get; set; }
        public int Volume { get; set; }
        public required ICollection<Creator> Creators { get; set; }
    }
}
