using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.DAL.Domain
{
    public class Multiplex
    {
        public Multiplex()
        {
            Shows = new HashSet<Show>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public ICollection<Show> Shows { get; set; }
    }
}
