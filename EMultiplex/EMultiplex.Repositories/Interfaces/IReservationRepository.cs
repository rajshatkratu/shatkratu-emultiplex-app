using EMultiplex.DAL.Domain;
using EMultiplex.Repositories.Interfaces;

namespace Multiplex.Api.Repositories.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        //(Reservation Result, bool IsSuccess, string ErrorResponse) CreateBooking(BookTicketRequest request);
    }
}
