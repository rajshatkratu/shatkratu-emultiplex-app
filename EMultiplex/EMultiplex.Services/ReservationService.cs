using EMultiplex.Models;
using EMultiplex.Models.Requests;
using EMultiplex.Repositories.Interfaces;
using EMultiplex.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<(ReservationModel Reservation, bool IsSuccess, string ErrorMessage)> CreateReservationAsync(ReservationRequest request)
        {
            var result = await _unitOfWork.ReservationRepository.CreateReservationAsync(request);

            if (result.IsSuccess)
                await _unitOfWork.SaveAsync();

            return result;
        }
    }
}
