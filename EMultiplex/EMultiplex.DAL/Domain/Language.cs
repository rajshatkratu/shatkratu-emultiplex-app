using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.DAL.Domain
{
    public class Language
    {
        public Language()
        {
            Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }

    }
}
