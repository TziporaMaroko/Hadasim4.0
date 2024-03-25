using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hadasim4._0.Data;
namespace Hadasim4._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Hadasim4_0Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Hadasim4_0Context") ?? throw new InvalidOperationException("Connection string 'Hadasim4_0Context' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Members}/{action=Index}/{id?}");

            app.Run();
        }
    }
}