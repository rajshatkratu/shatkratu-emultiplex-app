namespace EMultiplex.Models.Requests
{
    public class MovieCreateRequest
    {
        public string Name { get; set; }

        public int GenreId { get; set; }

        public int LanguageId { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }
    }
}
