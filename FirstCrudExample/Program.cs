using Microsoft.EntityFrameworkCore;

namespace FirstCrudExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
                {
                    options.LoginPath = "/Home/Login";
                    options.LogoutPath = "/Home/Logout";
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.ExpireTimeSpan=TimeSpan.FromDays(1);
                });
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<Models.LunarDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConString")));

            builder.Services.AddControllersWithViews();
            var app = builder.Build();            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllerRoute(name: "defult", pattern: "{controller=Home}/{action=Login}/{id?}");


            app.Run();
        }
    }
}
