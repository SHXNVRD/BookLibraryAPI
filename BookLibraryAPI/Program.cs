using BookLibraryAPI.Authorization.Handlers;
using BookLibraryAPI.Authorization.Requriements;
using BookLibraryAPI.Extensions;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Application.Services;
using Application.Interfaces;
using Application.Mappers;

namespace BookLibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<BookLibraryDbContext>(options =>
                {
                    options.UseSqlServer(config.GetConnectionString(nameof(BookLibraryDbContext)));
                });

            var jwtOptions = config
                .GetSection("JwtOptions")
                .Get<JwtOptions>();

            var basePath = AppContext.BaseDirectory;

            builder.Services.Configure<JwtOptions>(options =>
            {
                options.PrivateKeyPath = Path.Combine(basePath, jwtOptions.PrivateKeyPath);
                options.PublicKeyPath = Path.Combine(basePath, jwtOptions.PublicKeyPath);
                options.Issuer = jwtOptions.Issuer;
                options.Audience = jwtOptions.Audience;
                options.ExpiresHours = jwtOptions.ExpiresHours;
            });

            builder.Services.AddTransient<IGenreService, GenreService>();
            builder.Services.AddTransient<IBookService, BookService>();
            builder.Services.AddTransient<IBooksRepository, BooksRepository>();
            builder.Services.AddTransient<IGenresRepository, GenresRepository>();
            builder.Services.AddTransient<IAuthorsRepository, AuthorsRepository>();
            builder.Services.AddTransient<IBookGenresRepository, BookGenresRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddSingleton<IAuthorizationHandler, RoleHandler>();

            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureJwtAuthentication(config);
            builder.Services.AddAuthorization(policy =>
            {
                policy.AddPolicy("User", policy => policy.Requirements.Add(new RoleRequirement(["user"])));
                policy.AddPolicy("Admin", policy => policy.Requirements.Add(new RoleRequirement(["admin"])));
            });

            BookMappingConfig.RegisterMappings();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
