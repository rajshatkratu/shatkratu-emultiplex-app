using EMultiplex.DAL.Domain;
using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMultiplex.Services.Interfaces
{
    public interface IMovieService
    {
        Task<(Movie response, bool IsSuccess, string ErrorMessage)> CreateMovieAsync(MovieCreateRequest request);

        Task<IEnumerable<MovieSearchResponse>> GetMoviesAsync(string city, string genre, string language);

    }
}
