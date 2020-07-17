using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using EMultiplex.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EMultiplex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }

  
        [HttpPost("show")]
        public async Task<IActionResult> GetMoviesAsync(string city, string genre, string language)
        {
            var result = await _movieService.GetMoviesAsync(city, genre, language);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMovieAsync([FromBody] MovieCreateRequest request)
        {
            var result = await _movieService.CreateMovieAsync(request);

            if (!result.IsSuccess)
            {
                return BadRequest(
                    new ErrorResponse
                    {
                        Errors = new[] { result.ErrorMessage ?? "Error occured while updating the record." }
                    });
            }


            return Ok(result.response);
        }

        

    }
}
