using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using EMultiplex.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Multiplex.Api.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMultiplex.API.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    [Produces("application/json")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReservationAsync([FromBody] ReservationRequest request)
        {
            var result = await _reservationService.CreateReservationAsync(request);

            if (!result.IsSuccess)
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Errors = new[] { result.ErrorMessage ?? "Error occured while updating the record." }
                    });
            }
            return Ok(result.Reservation);
        }
    }
}
