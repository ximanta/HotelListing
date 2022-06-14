using APIGateway.Services;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
/*Load ocelot.json for route configuration*/
builder.Configuration.AddJsonFile("ocelot.json");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
/*Registered the 'Ocelot' middleware.*/
app.UseOcelot().Wait();
app.Run();

