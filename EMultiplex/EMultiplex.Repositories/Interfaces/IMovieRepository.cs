using EMultiplex.DAL.Domain;
using EMultiplex.Models;
using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using Multiplex.Api.Contracts.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMultiplex.Repositories.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<(MovieModel Result, bool IsSuccess, string ErrorMessage)> CreateMovieAsync(MovieCreateRequest request);

        Task<(IEnumerable<MovieSearchResponse> Result, bool IsSuccess, string ErrorMessage)> GetMoviesAsync(MovieSearchRequest request);

    }
}
