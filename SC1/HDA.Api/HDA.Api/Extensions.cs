using HDA.Business.Identity;
using HDA.Business.Interfaces;
using HDA.Business.Services;
using HDA.Domain;
using HDA.Domain.Interfaces;
using HDA.Domain.Repositories;
using HDA.Domain.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HDA.Api
{
    public static class Extensions
    {
        public static void LoadConfiguration(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var identitySection = configurationManager.GetSection("Identity");
            services.Configure<IdentitySettings>(identitySection);
        }
        public static void ConfigureDatabase(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            // configure DatabaseContext with serviceProvider and optionBuilder
            services.AddDbContext<MyDbContext>((provider, options) =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("SoccerConnectionString"));
            });
        }
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            //Dependency Injection
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IMatchService, MatchService>();

            services.AddScoped<IJoueurMatchRepository, JoueurMatchRepository>();
            services.AddScoped<IJoueurMatchService, JoueurMatchService>();

            services.AddScoped<IJoueurRepository, JoueurRepository>();
            services.AddScoped<IJoueurService, JoueurService>();

            services.AddScoped<IFormuleRepasRepository, FormuleRepasRepository>();
            services.AddScoped<IFormuleRepasService, FormuleRepasService>();

            services.AddScoped<IMembreZeroRepository, MembreZeroRepository>();
            services.AddScoped<IMembreZeroService, MembreZeroService>();

            services.AddScoped<IMembreRepository, MembreRepository>();
            services.AddScoped<IMembreService, MembreService>();

            services.AddScoped<IResasZeroRepository, ResasZeroRepository>();
            services.AddScoped<IResasZeroService, ResasZeroService>();

            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IMailSoccerService, MailSoccerService>();

            services.AddControllers();
            services.AddHttpContextAccessor();
        }

        public static void ConfigureIdentity(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var identitySettings = configurationManager.GetSection("Identity").Get<IdentitySettings>();

            //ajout Identity et Roles
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders();

            //Authentication for jwt authentication scheme
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = identitySettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = identitySettings.Audience,

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddEndpointsApiExplorer();

            //configuration de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hub des Arts - v1", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Ajouter le token ainsi : \"Bearer xxxx\" où xxxx est votre token d'authentification",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[]{}
                    }
                });
            });
        }

    }
}