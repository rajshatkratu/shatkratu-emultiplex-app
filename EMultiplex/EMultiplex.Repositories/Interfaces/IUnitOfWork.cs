using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IMovieRepository MovieRepository { get; }
        Task<int> SaveAsync();
    }
}
