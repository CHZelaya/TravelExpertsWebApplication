using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelExpertsData.Models;
using TravelExpertsMVC.EmailService;

namespace TravelExpertsMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add email service
            builder.Services.AddTransient<IEmailSender, EmailSenderService>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //add service for creation of context object
            builder.Services.AddDbContext<TravelExpertsContext>(options =>
                        options.UseSqlServer(
                            builder.Configuration.GetConnectionString("TravelExpertsContext")));

            //add configuration for email settings
            builder.Services
                .Configure<EmailSettings>
                (builder.Configuration.GetSection("EmailSettings"));


            //add identity service
            builder.Services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<TravelExpertsContext>().AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //authentication before authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
