using EMultiplex.DAL.Domain;
using EMultiplex.Models;
using EMultiplex.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Services.Interfaces
{
    public interface IReservationService
    {
        Task<(ReservationModel reservation, bool IsSuccess, string ErrorMessage)> CreateReservationAsync(ReservationRequest request);
    }
}
