using JavascriptLearningTool.Components;
using JavascriptLearningTool.Repositories;
using JavascriptLearningTool.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;

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

            app.Run();
        }

        private static void RegisterServices(IServiceCollection services, IConfigurationManager configurationManager)
        {
            // Services
            services.AddScoped<UserService>();

            // Repositories
            services.AddScoped<UserProgressRepository>();
            services.AddScoped<UserRepository>();

            // Database
            services.AddTransient<IDbConnection>(sp => new SqlConnection(configurationManager.GetConnectionString("DefaultConnection")));
            services.AddSingleton<DbConnectionFactory>();

            services.AddScoped<UserAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();

            services.AddCascadingAuthenticationState();
            services.AddAuthorizationCore();
        }
    }
}
