using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Railway_Reservation_System.Data;
using Railway_Reservation_System.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.Xml;
using railway_reservation_system.repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "RailwayAPI", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In=ParameterLocation.Header,
        Description="Enter A valid JWT Token",
        Name="JWT Authentication",
        Type=SecuritySchemeType.Http,
        BearerFormat="JWT",
        Scheme="bearer"
        
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
   });
});
builder.Services.AddEndpointsApiExplorer();




builder.Services.AddDbContext<RailwayRSDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RRSDatabase"));
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();


builder.Services.AddScoped<ITrainRespository, TrainRepository>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

builder.Services.AddScoped<IAdminRepository,AdminRepository>();


builder.Services.AddScoped<ITokenHandler, Railway_Reservation_System.Repositories.TokenHandler>();


builder.Services.AddCors(option => option.AddPolicy(name: "NgOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API"));
}

app.UseCors("NgOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
