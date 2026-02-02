namespace MangaTracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public ICollection<Manga>? Mangas { get; set; }
    }
}
