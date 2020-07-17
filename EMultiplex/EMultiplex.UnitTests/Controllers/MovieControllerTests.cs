using EMultiplex.API.Controllers;
using EMultiplex.DAL.Domain;
using EMultiplex.Models.Responses;
using EMultiplex.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Multiplex.Api.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EMultiplex.UnitTests.Controllers
{
    public class MovieControllerTests
    {

        protected MovieController controllerUnderTest { get; }
        protected Mock<IMovieService> movieServiceMock { get; }

        
        public MovieControllerTests()
        {
            movieServiceMock = new Mock<IMovieService>();
            controllerUnderTest = new MovieController(movieServiceMock.Object);
        }


        [Fact]
        public async Task GetMoviesAsync_OnValidArgument_ReturnSuccessResult()
        {
            var movies = new List<MovieSearchResponse> { 
                new MovieSearchResponse { 
            
                  MovieId= 1,
                  ShowId =1
            } };

            var data = new MovieSearchRequest
            {
                City = "Bengaluru"
            };

            movieServiceMock.Setup(x => x.GetMoviesAsync(data))
                    .ReturnsAsync((movies, true, null));

            // Act 
            var result = await controllerUnderTest.GetMoviesAsync(data);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(movies, createdResult.Value);
        }

        [Fact]
        public async Task GetMoviesAsync_OnEmptyArgument_ReturnOkResult()
        {
            var movies = new List<MovieSearchResponse> {
                new MovieSearchResponse {

                  MovieId= 1,
                  ShowId =1
            } };
            var data = new MovieSearchRequest();
            movieServiceMock.Setup(x => x.GetMoviesAsync(data))
                    .ReturnsAsync((movies, true, null));

            // Act 
            var result = await controllerUnderTest.GetMoviesAsync(data);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(movies, createdResult.Value);
        }

        [Fact]
        public async Task GetMoviesAsync_OnInvalidArgument_ReturnBadResult()
        {
            string errorMessage = "Error Occured";
            var erroResult = new ErrorResponse { Errors = new[] { errorMessage } };
            MovieSearchRequest data = null;
            movieServiceMock.Setup(x => x.GetMoviesAsync(data))
                    .ReturnsAsync((null, false, errorMessage));

            // Act 
            var result = await controllerUnderTest.GetMoviesAsync(data);

            // Assert
            var createdResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, createdResult.StatusCode);
            var error = (createdResult.Value as ErrorResponse).Errors.FirstOrDefault();
            Assert.Equal(errorMessage, error);
        }
    }
}
