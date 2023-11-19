using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IptFinals.Data;
using IptFinals.Areas.Identity.Data;
using Microsoft.Identity.Client;
namespace IptFinals.Controllers
{ 
    public class Program
    {
       
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("IptDbContextConnection") ?? throw new InvalidOperationException("Connection string 'IptDbContextConnection' not found.");

            builder.Services.AddDbContext<IptDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IptUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IptDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
            });
            //CreateHostBuilder(args).Build().Run();

             IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                   webBuilder.UseWebRoot("wwwroot");
               });
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    var roles = new[] { "Admin", "Manager", "Student" };

            //    foreach (var role in roles)
            //    {

            //        if (!await roleManager.RoleExistsAsync(role))
            //            await roleManager.CreateAsync(new IdentityRole(role));
            //    }
            //}
            ////using (var scope = app.Services.CreateScope())
            //{
            //    var userManager =
            //        scope.ServiceProvider.GetRequiredService<UserManager<IptUser>>();

            //    string email = "admin@admin.com";
            //    string password = "Test1234,";

            //    if (await userManager.FindByEmailAsync(email) == null)
            //    {
            //        var user = new IptUser();
     


            //        await userManager.CreateAsync(user, password);

            //        await userManager.AddToRoleAsync(user, "Admin");
            //    }
            //}
            app.Run();
        }
    }


}

