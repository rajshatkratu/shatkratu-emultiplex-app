using EMultiplex.DAL;
using EMultiplex.Repositories.Interfaces;
using Multiplex.Api.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly EMultiplexDbContext _context;
        public IMovieRepository MovieRepository { get; private set; }

        public UnitOfWork(EMultiplexDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            MovieRepository = new MovieRepository(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
