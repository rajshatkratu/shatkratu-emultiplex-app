using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.DAL.Domain
{
    public class Movie
    {
        public Movie()
        {
            Shows = new HashSet<Show>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int GenreId { get; set; }

        public int LanguageId { get; set; }

        public Genre Genre { get; set; }

        public Language Language { get; set; }

        public ICollection<Show> Shows { get; set; }
    }
}
