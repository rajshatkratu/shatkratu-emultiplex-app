using EMultiplex.DAL;
using EMultiplex.DAL.Domain;
using EMultiplex.Repositories;
using Multiplex.Api.Repositories.Interfaces;

namespace Multiplex.Api.Repositories.Implementations
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(EMultiplexDbContext context) : base(context)
        {

        }

        //public (Reservation Result, bool IsSuccess, string ErrorResponse) CreateBooking(BookTicketRequest request)
        //{

        //    var show = _context.Shows.Include(x => x.Movie).Include(x=>x.Theater).FirstOrDefault(x => x.Id == request.ShowId);


        //    if (show == null)
        //        return (null, false, "Invalid show");

        //    if(!request.MovieName.Equals(show.Movie.Name, StringComparison.OrdinalIgnoreCase))
        //        return (null, false, "Invalid movie request for show");

        //    if (!request.TheaterName.Equals(show.Theater.Name, StringComparison.OrdinalIgnoreCase))
        //        return (null, false, "Invalid theater request for show");

        //    if (request.NoOfTickets > show.AvailableSeats)
        //        return (null, false, "The no of requested sheats are not available to book");

        //    if ((request.ShowDate != show.ShowDate) && (request.ShowDate <= DateTime.UtcNow.AddDays(-1)))
        //        return (null, false, "Incorrect show date");


        //    var reservation = new Reservation
        //    {
        //        MaxSeatPerBooking = 5,
        //        BookingDate = show.ShowDate,
        //        NumberOfSeatBooked = request.NoOfTickets,
        //        ShowId = show.Id,
        //        PriceOfBooking = show.PricePerSeat * request.NoOfTickets,
        //        UserId = request.UserId
        //    };

        //    base.Add(reservation);
        //    show.AvailableSeats = show.AvailableSeats - request.NoOfTickets;
        //    _context.Shows.Update(show);

        //    return (reservation, true, null);
        //}
    }
}
