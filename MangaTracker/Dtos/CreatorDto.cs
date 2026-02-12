using MangaTracker.Models;

namespace MangaTracker.Dtos
{
    public class CreatorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsAuthor { get; set; }
        public bool IsIllustrator { get; set; }
        public ICollection<MangaDto> Mangas { get; set; } = [];
    }
}
