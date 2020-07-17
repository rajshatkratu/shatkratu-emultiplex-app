using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace EMultiplex.DAL.Domain
{
    public class City
    {
        public City()
        {
            Multiplexes = new HashSet<Multiplex>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Multiplex> Multiplexes { get; set; }
    }
}
