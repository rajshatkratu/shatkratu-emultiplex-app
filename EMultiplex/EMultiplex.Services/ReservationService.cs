using EMultiplex.Models;
using EMultiplex.Models.Requests;
using EMultiplex.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Services
{
    public class ReservationService : IReservationService
    {
        public Task<(ReservationModel reservation, bool IsSuccess, string ErrorMessage)> CreateReservationAsync(ReservationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
