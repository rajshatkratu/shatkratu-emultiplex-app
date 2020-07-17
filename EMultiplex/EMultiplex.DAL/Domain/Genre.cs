using System.Collections.Generic;

namespace EMultiplex.DAL.Domain
{
    public class Genre
    {
        public Genre()
        {
            Movies = new HashSet<Movie>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
