using EMultiplex.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EMultiplex.API.Utils
{
    public class EmultiplexExceptionMiddleware
    {
    
        private readonly RequestDelegate _next;
        private readonly ILogger<EmultiplexExceptionMiddleware> _logger;

        public EmultiplexExceptionMiddleware(RequestDelegate next, ILogger<EmultiplexExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await GetException(ex, context);
            }
        }
        private Task GetException(Exception ex, HttpContext context)
        {
            string errorMessage = string.Empty;

            var errorResponse = new ErrorResponseModel
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ex.GetBaseException().Message
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorException = JObject.Parse(JsonConvert.SerializeObject(errorResponse));
            return context.Response.WriteAsync(errorException.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class EmultiplexExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseEmultiplexExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EmultiplexExceptionMiddleware>();
        }
    }
}
