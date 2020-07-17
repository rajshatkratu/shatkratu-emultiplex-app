using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.DAL.Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string UserId { get; set; }
        public int ShowId { get; set; }
        public int MaxSeatPerBooking { get; set; }
        public int NumberOfSeatBooked { get; set; }
        public double PriceOfBooking { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual IdentityUser User { get; set; }

        public virtual Show Show { get; set; }

    }
}
