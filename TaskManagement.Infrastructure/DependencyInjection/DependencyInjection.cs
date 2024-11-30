using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Infrastructure.Authentication;
using TaskManagement.Infrastructure.Context;
using TaskManagement.Infrastructure.Repositories;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultDb"),
                b => b.MigrationsAssembly(typeof(DependencyInjection).Assembly.FullName)));

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,                    
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),                    
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context => 
                    {
                        context.Token = context.Request.Cookies[jwtOptions.CookieName];

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddLogging(options =>
            {
                options.AddConsole(c =>
                {                    
                    c.TimestampFormat = "[HH:mm:ss] ";
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();            

            return services;
        }
    }
}
