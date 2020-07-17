using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GenreId { get; set; }

        public int LanguageId { get; set; }

    }
}
