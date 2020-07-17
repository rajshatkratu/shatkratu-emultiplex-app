using EMultiplex.DAL.Domain;
using EMultiplex.Models;
using EMultiplex.Models.Requests;
using EMultiplex.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Multiplex.Api.Repositories.Interfaces
{
    public interface IReservationRepository : IGenericRepository<Reservation>
    {
        Task<(ReservationModel Reservation, bool IsSuccess, string ErrorMessage)> CreateReservationAsync(ReservationRequest request);
    }
}
