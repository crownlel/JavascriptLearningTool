using JavascriptLearningTool.ClientServices;
using JavascriptLearningTool.Components;
using JavascriptLearningTool.Helpers;
using JavascriptLearningTool.Models;
using JavascriptLearningTool.Repositories;
using JavascriptLearningTool.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

namespace JavascriptLearningTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            RegisterServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }

        private static void RegisterServices(IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddControllers();

            // Services
            services.AddScoped<UserService>();
            services.AddScoped<ApiService>();

            // Repositories
            services.AddScoped<UserRepository>();
            services.AddScoped<CourseRepository>();
            services.AddScoped<UserProgressRepository>();
            services.AddScoped<PageRepository>();

            // Database
            services.AddTransient<IDbConnection>(sp => new SqlConnection(configurationManager.GetConnectionString("DefaultConnection")));
            services.AddSingleton<DbConnectionFactory>();

            services.AddScoped<UserAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7120") });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configurationManager["Jwt:ValidIssuer"],
                    ValidAudience = configurationManager["Jwt:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["Jwt:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddSingleton(new JwtCredentials
            {
                Issuer = configurationManager["Jwt:ValidIssuer"],
                Audience = configurationManager["Jwt:ValidAudience"],
                Key = configurationManager["Jwt:Secret"]
            });
            services.AddCascadingAuthenticationState();
            services.AddAuthorization();
        }
    }
}
