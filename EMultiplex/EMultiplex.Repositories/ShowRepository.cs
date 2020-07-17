using EMultiplex.DAL;
using EMultiplex.DAL.Domain;
using EMultiplex.Repositories;
using EMultiplex.Repositories.Interfaces;

namespace Multiplex.Api.Repositories.Implementations
{
    public class ShowRepository : GenericRepository<Show>,IShowRepository
    {
        public ShowRepository(EMultiplexDbContext context) : base(context)
        {

        }
    }
}
