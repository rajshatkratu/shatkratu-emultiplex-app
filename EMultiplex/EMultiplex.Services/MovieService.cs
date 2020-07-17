using EMultiplex.DAL.Domain;
using EMultiplex.Models;
using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using EMultiplex.Repositories.Interfaces;
using EMultiplex.Services.Interfaces;
using Multiplex.Api.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMultiplex.Services
{
    public class MovieService : IMovieService
    {
        private IUnitOfWork _unitOfWork;

        public MovieService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<(MovieModel Result, bool IsSuccess, string ErrorMessage)> CreateMovieAsync(MovieCreateRequest request)
        {
            var result = await _unitOfWork.MovieRepository.CreateMovieAsync(request);
            await _unitOfWork.SaveAsync();
            return result;
        }

        public async Task<(IEnumerable<MovieSearchResponse> Result, bool IsSuccess, string ErrorMessage)> GetMoviesAsync(MovieSearchRequest request)
        {
            return await _unitOfWork.MovieRepository.GetMoviesAsync(request);
        }
    }
}
