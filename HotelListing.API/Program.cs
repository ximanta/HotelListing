using HotelListing.API.config;
using HotelListing.API.data;
using HotelListing.API.Middleware;
using HotelListing.API.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
// Add services to the container.
builder.Services.AddDbContext<HotelListDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
options.AddPolicy("AllowAll",
    b => b.AllowAnyHeader()
          .AllowAnyMethod()
          .AllowAnyOrigin());

});

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddAutoMapper(typeof(MapperConfig));
/*The AddScoped method registers the service with a scoped lifetime, the lifetime of a single request.*/
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(ICountryRepository), typeof(CountryRepository));
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024;
    options.UseCaseSensitivePaths = true;
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCaching();

app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl =
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(30)
        };
    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
        new string[] { "Accept-Encoding" };

    await next();
});
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
