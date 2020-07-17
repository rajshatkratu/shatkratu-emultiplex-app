using System;

namespace EMultiplex.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string UserId { get; set; }
        public int ShowId { get; set; }
        public int MaxSeatPerBooking { get; set; }
        public int NumberOfSeatBooked { get; set; }
        public double PriceOfBooking { get; set; }

    }
}
