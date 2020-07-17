using System;
using System.ComponentModel.DataAnnotations;

namespace EMultiplex.Models.Requests
{
    public class ReservationRequest
    {
        [Range(1,5)]
        public int NoOfTickets { get; set; }
        [Required]
        public string MovieName { get; set; }
        public int MovieId { get; set; }
        [Required]
        public int ShowId { get; set; }
        public int MultiplexId { get; set; }
        [Required]
        public string MultiplexName { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public DateTime ShowDate { get; set; }
        public string UserId { get; set; }
    }
}
