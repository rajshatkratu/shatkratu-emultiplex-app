using System;

namespace EMultiplex.Models.Requests
{
    public class ReservationRequest
    {
        public int NoOfTickets { get; set; }
        public string MovieName { get; set; }
        public int MovieId { get; set; }
        public int ShowId { get; set; }
        public int TheaterId { get; set; }
        public string TheaterName { get; set; }
        public DateTime ShowDate { get; set; }
        public string UserId { get; set; }
    }
}
