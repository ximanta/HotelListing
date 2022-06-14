using APIGateway.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace HotelListing.API.Middleware
{
    /*Middleware object to be registered in the application pipeline in Program.cs*/
    public class JwtValidatorMiddleware
    {
    
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtValidatorMiddleware> _logger;
        private readonly AppSettings _appSettings;

        /*Dependency injection of RequestDelegate and Logger*/
        public JwtValidatorMiddleware(RequestDelegate next, ILogger<JwtValidatorMiddleware> logger, AppSettings appSettings)
        {
            this._next = next;
            this._logger = logger;
            this._appSettings = appSettings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            /*Global try catch for all requests*/
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (token != null)

                    if (validate(context, token))
                    {
                        /*Await result for the next operation relative to the request*/
                        await _next(context);
                    }
                    else
                    {
                        _logger.LogError($"Token Validation failed while processing {context.Request.Path}");
                        throw new Exception("Token Validation failed");
                    }
             
            }
             catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went wrong while processing {context.Request.Path}");


            }
        }
            private bool validate(HttpContext context, string token)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                return true;
                       
                }
                catch
                {
                    // do nothing if jwt validation fails
                }
            return true;
            }

       

        }
    }

