using EMultiplex.Common.Constants;
using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using EMultiplex.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Multiplex.Api.Contracts.Requests;
using System;
using System.Threading.Tasks;

namespace EMultiplex.API.Controllers
{
    [ApiController]
    [Route("api/movies")]
    [Produces("application/json")]

    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }

  
        [HttpPost("search")]
        public async Task<IActionResult> GetMoviesAsync([FromBody] MovieSearchRequest request)
        {
            var response = await _movieService.GetMoviesAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Errors = new[] { response.ErrorMessage ?? EMultiplexConstants.ErrorOccured }
                    });
            }

            return Ok(response.Result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateMovieAsync([FromBody] MovieCreateRequest request)
        {
            var response = await _movieService.CreateMovieAsync(request);

            if (!response.IsSuccess)
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Errors = new[] { response.ErrorMessage ?? EMultiplexConstants.ErrorOccured }
                    });
            }


            return Ok(response.Result);
        }
    }
}
