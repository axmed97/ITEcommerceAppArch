
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolver;

var builder = WebApplication.CreateBuilder(args);

// Service Registrations
builder.Services.AddBusinessService();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductService, ProductManagerV2>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
