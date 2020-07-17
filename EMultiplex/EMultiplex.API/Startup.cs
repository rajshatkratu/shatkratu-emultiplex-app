using EMultiplex.DAL;
using EMultiplex.Models.Options;
using EMultiplex.Repositories;
using EMultiplex.Repositories.Interfaces;
using EMultiplex.Services;
using EMultiplex.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;

namespace EMultiplex.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EMultiplexDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("EMultiplexDbConnection")));
            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EMultiplexDbContext>();

            services.AddMvc();

            // services
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IReservationService, ReservationService>();


            // Identity and UnitOfWork 
            services.AddScoped<IIdentityRespository, IdentityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var jwtOptions = new JwtOptions();
            Configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
            services.AddSingleton(jwtOptions);
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtOptions.Secret))
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
            {
                option.SaveToken = true;

                option.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization();

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Multiplex Api App", Version = "v1" });

                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                };
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Api jwt bearer authentication for swagger",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseStaticFiles();



            var swaggerOptions = new SwaggerOptions();

            Configuration.GetSection(nameof(swaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions.RouteJson;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
