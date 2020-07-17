using EMultiplex.DAL;
using EMultiplex.DAL.Domain;
using EMultiplex.Models;
using EMultiplex.Models.Requests;
using EMultiplex.Repositories;
using Microsoft.EntityFrameworkCore;
using Multiplex.Api.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Multiplex.Api.Repositories.Implementations
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(EMultiplexDbContext context) : base(context)
        {

        }

        public async Task<(ReservationModel Reservation, bool IsSuccess, string ErrorMessage)> CreateReservationAsync(ReservationRequest request)
        {
            var show = await Context.Shows.Include(x=> x.Movie).Include(x=> x.Multiplex).ThenInclude(x=>x.City).FirstOrDefaultAsync(x => x.Id == request.ShowId);


            if (show == null)
                return (null, false, "Invalid show");

            if (!request.MovieName.Equals(show.Movie.Name, StringComparison.OrdinalIgnoreCase))
                return (null, false, "Invalid movie request for show");

            if (!request.CityName.Equals(show.Multiplex.City.Name, StringComparison.OrdinalIgnoreCase))
                return (null, false, "Invalid multiplex city request for show");

            if (!request.MultiplexName.Equals(show.Multiplex.Name, StringComparison.OrdinalIgnoreCase))
                return (null, false, "Invalid multiplex request for show");

            if (request.NoOfTickets > show.AvailableSeats)
                return (null, false, "The no of requested sheats are not available to book");

            if ((request.ShowDate != show.ShowDate) && (request.ShowDate <= DateTime.UtcNow.AddDays(-1)))
                return (null, false, "Incorrect show date");


            var reservation = new Reservation
            {
                MaxSeatPerBooking = 5,
                BookingDate = DateTime.UtcNow,
                NumberOfSeatBooked = request.NoOfTickets,
                ShowId = show.Id,
                PriceOfBooking = show.PricePerSeat * request.NoOfTickets,
                UserId = request.UserId
            };

            await AddAsync(reservation);
            show.AvailableSeats = show.AvailableSeats - request.NoOfTickets;
            Context.Shows.Update(show);

            ReservationModel model = GetReservationModel(reservation);

            return (model, true, null);
        }

        private ReservationModel GetReservationModel(Reservation reservation)
        {
            return new ReservationModel
            {
                Id = reservation.Id,
                ShowId = reservation.ShowId,
                UserId = reservation.UserId,
                PriceOfBooking = reservation.PriceOfBooking,
                MaxSeatPerBooking = reservation.MaxSeatPerBooking,
                BookingDate = reservation.BookingDate,
                NumberOfSeatBooked = reservation.NumberOfSeatBooked
            };
        }
    }
}
