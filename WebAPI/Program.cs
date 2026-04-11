
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebAPI.Middewares;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddProblemDetails();
// Service Registrations
builder.Services.AddBusinessService();

builder.Services.AddCors(option => option.AddPolicy("Policy", policy =>
{
    policy
    .AllowAnyHeader()
    .WithOrigins("http://localhost:4200", "https://decorilla.az")
    .AllowCredentials()
    .AllowAnyMethod();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Token:SecretKey"]!)),

        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Development
// Preproduction ?
// Production

app.UseCors("Policy");
app.UseHttpsRedirection();
//app.UseExceptionHandler();


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();