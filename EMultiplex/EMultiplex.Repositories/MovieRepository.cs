using EMultiplex.Common.Constants;
using EMultiplex.DAL;
using EMultiplex.DAL.Domain;
using EMultiplex.Models.Requests;
using EMultiplex.Models.Responses;
using EMultiplex.Repositories;
using EMultiplex.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Multiplex.Api.Repositories.Implementations
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {

        public MovieRepository(EMultiplexDbContext context) : base(context)
        {
        }

        public async Task<(Movie response, bool IsSuccess, string ErrorMessage)> CreateMovieAsync(MovieCreateRequest request)
        {
            var genre = Context.Genres.FirstOrDefault(x => x.Name.Equals(request.Genre, StringComparison.OrdinalIgnoreCase));

            if (genre == null)
                return (null, false, string.Format(EMultiplexConstants.ArgumentDoesNotExistFormat, nameof(genre)));

            var language = Context.Languages.FirstOrDefault(x => x.Name.Equals(request.Language, StringComparison.OrdinalIgnoreCase));

            if (language == null)
                return (null, false, string.Format(EMultiplexConstants.ArgumentDoesNotExistFormat, nameof(language)));

            var movie = new Movie
            {
                GenreId = genre.Id,
                LanguageId = language.Id,
                Name = request.Name
            };

            await AddAsync(movie);

            return (movie, true, null);
        }

        public async Task<IEnumerable<MovieSearchResponse>> GetMoviesAsync(string city, string genre, string language)
        {
            Func<string, string, bool> predicate = (x, y) =>
            {
                if (string.IsNullOrEmpty(y))
                    return true;
                return x.Equals(y, StringComparison.OrdinalIgnoreCase);
            };

            var movies = await (from m in Context.Movies
                         join g in Context.Genres on m.GenreId equals g.Id
                         join l in Context.Languages on m.LanguageId equals l.Id
                         join s in Context.Shows on m.Id equals s.MovieId
                         join ml in Context.Multiplexes  on s.MultiplexId equals ml.Id
                         join c in Context.Cities on ml.CityId equals c.Id
                         where predicate(g.Name, genre) && predicate(l.Name, language) && predicate(c.Name, city)
                         select new 
                         {
                             MovieId = m.Id,
                             MovieName = m.Name,
                             Genre = g.Name,
                             Language = l.Name,
                             ShowDate = s.ShowDate,
                             PricePerSeat = s.PricePerSeat,
                             MultiplexId = s.MultiplexId,
                             MultiplexName = ml.Name,
                             City = c.Name,
                             ShowId = s.Id
                         }).Distinct().ToListAsync();

            return movies.Select(x=> new MovieSearchResponse{
                MovieId = x.MovieId,
                MovieName = x.MovieName,
                Genre = x.Genre,
                Language = x.Language,
                ShowDate = x.ShowDate,
                PricePerSeat = x.PricePerSeat,
                MultiplexId = x.MultiplexId,
                MultiplexName = x.MultiplexName,
                City = x.City,
                ShowId= x.ShowId
            });
        }
    }
}
