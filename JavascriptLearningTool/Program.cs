using JavascriptLearningTool.Components;
using JavascriptLearningTool.Repositories;
using JavascriptLearningTool.Services;
using Microsoft.AspNetCore.Components.Authorization;

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

            RegisterServices(builder.Services);

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

        private static void RegisterServices(IServiceCollection services)
        {

            services.AddSingleton<UserService>();
            services.AddSingleton<UserRepository>();
            services.AddScoped<UserAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, UserAuthenticationStateProvider>();

            services.AddCascadingAuthenticationState();
            services.AddAuthorizationCore();
        }
    }
}
