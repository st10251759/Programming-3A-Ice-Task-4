using Microsoft.EntityFrameworkCore;
using Hotel_Booking_Prog_7311_Ice_Task_4.Data;

namespace Hotel_Booking_Prog_7311_Ice_Task_4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Adding DB Context with SQL Server connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Prog7311DEV"),
                    sqlOptions => sqlOptions.MigrationsAssembly("Hotel_Booking_Prog_7311_Ice_Task_4")));

            // If you have authentication, add it here
            // builder.Services.AddAuthentication().AddCookie(options => { ... });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  // Show detailed errors in development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // If you're using authentication, make sure this line comes before Authorization
            // app.UseAuthentication();  

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
