using HotelListing.API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace HotelListing.API.Middleware
{
    /*Middleware object to be registered in the application pipeline in Program.cs*/
    public class ExceptionMiddleware
    {
    
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        /*Dependency injection of RequestDelegate and Logger*/
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            /*Global try catch for all requests*/
            try
            {
                /*Await result for the next operation relative to the request*/
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went wrong while processing {context.Request.Path}");
                /*Handle the exception */
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            /*Default status code - worst case scenario*/
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDeatils
            {
                ErrorType = "Failure",
                ErrorMessage = ex.Message,
            };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorDetails.ErrorType = "Bad Request";
                    break;
                case AlreadyExistsException allreadyExistsException:
                    statusCode = HttpStatusCode.Conflict;
                    errorDetails.ErrorType = "Conflict - Already Exists";
                    break;
                default:
                    break;
            }

            string response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(response);
        }
    }

    public class ErrorDeatils
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}