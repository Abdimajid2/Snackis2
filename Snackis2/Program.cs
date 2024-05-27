using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Snackis2.DAL;
using Snackis2.Data;
namespace Snackis2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("Snackis2ContextConnection") ?? throw new InvalidOperationException("Connection string 'Snackis2ContextConnection' not found.");

            builder.Services.AddDbContext<Snackis2Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<Areas.Identity.Data.Snackis2User>(options => 
            options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Snackis2Context>();

            builder.Services.AddAuthorization(options =>
            options.AddPolicy("AdminKrav", policy => policy.RequireRole("Admin")));
 
            // Add services to the container.
            builder.Services.AddRazorPages(options => options.Conventions.AuthorizeFolder("/Admin", "AdminKrav"));

            builder.Services.AddSingleton<CategoryManager>();

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
