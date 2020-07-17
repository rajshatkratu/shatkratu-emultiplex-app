using System;

namespace EMultiplex.Models.Responses
{
    public class MovieSearchResponse
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public DateTime ShowDate { get; set; }
        public double PricePerSeat { get; set; }
        public int MultiplexId { get; set; }
        public string MultiplexName { get; set; }
        public string City { get; set; }
        public int ShowId { get; set; }
    }
}
