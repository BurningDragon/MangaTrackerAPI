namespace MangaTracker.Dtos
{
    public class CreateCreatorDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool IsWriter { get; set; }
        public bool IsIllustrator { get; set; }
    }
}
