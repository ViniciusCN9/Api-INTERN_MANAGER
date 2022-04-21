using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DesafioAPI.application.Interfaces;
using DesafioAPI.application.Services;
using DesafioAPI.domain.Repositories;
using DesafioAPI.domain.Settings;
using DesafioAPI.infra.Database.Context;
using DesafioAPI.infra.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DesafioAPI.api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddCors();
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = null);

            var key = Encoding.ASCII.GetBytes(SecretKey.secretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioAPI.api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
                { 
                    Name = "Authorization", 
                    Type = SecuritySchemeType.ApiKey, 
                    Scheme = "Bearer", 
                    BearerFormat = "JWT", 
                    In = ParameterLocation.Header, 
                    Description = "JWT Authorization header using the Bearer scheme." 
                }); 
                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                { 
                    { 
                          new OpenApiSecurityScheme 
                          { 
                              Reference = new OpenApiReference 
                              { 
                                  Type = ReferenceType.SecurityScheme, 
                                  Id = "Bearer" 
                              } 
                          }, 
                         new string[] {} 
                    } 
                });
            });

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IStarterRepository, StarterRepository>();
            services.AddScoped<IStarterService, StarterService>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailLocalService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioAPI.api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
