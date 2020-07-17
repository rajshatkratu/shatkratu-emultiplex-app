using EMultiplex.DAL;
using EMultiplex.DAL.Domain;
using EMultiplex.Repositories.Interfaces;

namespace EMultiplex.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(EMultiplexDbContext context) : base(context)
        {

        }
    }
}
