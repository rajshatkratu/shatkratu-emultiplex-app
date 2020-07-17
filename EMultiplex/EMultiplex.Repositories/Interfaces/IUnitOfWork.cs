using Multiplex.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IMovieRepository MovieRepository { get; }
        IReservationRepository ReservationRepository { get; }
        Task<int> SaveAsync();
    }
}
