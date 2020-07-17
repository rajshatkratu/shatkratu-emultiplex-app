using System;

namespace EMultiplex.Models
{
    public class ShowModel
    {
        public int Id { get; set; }
        public DateTime ShowDate { get; set; }
        public int AvailableSeats { get; set; }
        public int MaximumSeats { get; set; }
        public double PricePerSeat { get; set; }

        public int MovieId { get; set; }
        public int MultiplexId { get; set; }

    }
}
