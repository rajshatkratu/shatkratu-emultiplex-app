using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.DAL.Domain
{
    public class Show
    {
        public Show()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public DateTime ShowDate { get; set; }
        public int AvailableSeats { get; set; }
        public int MaximumSeats { get; set; }
        public double PricePerSeat { get; set; }

        public int MovieId { get; set; }
        public int MultiplexId { get; set; }
        public virtual Movie Movie { get; set; }

        public virtual Multiplex Multiplex { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

    }
}
