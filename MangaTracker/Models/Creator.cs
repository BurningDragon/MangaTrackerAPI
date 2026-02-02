namespace MangaTracker.Models
{
    public class Creator
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public  bool IsAuthor { get; set; }
        public bool IsIllustrator { get; set; }
        public ICollection<Manga>? Mangas { get; set; }
    }
}
