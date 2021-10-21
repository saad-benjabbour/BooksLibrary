using BooksLibrary.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using BooksData;
using BooksLibraryService;
namespace BooksLibrary
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // adding the razor pages
            services.AddRazorPages();

            services.AddPaging();

            // linking the interfaces with classes that implements those functions
            services.AddScoped<ILibraryAsset, LibraryAssetService>();
            services.AddScoped<IPatron, PatronService>();
            services.AddScoped<ICheckout, CheckoutService>();

            // getting a service for the database connection
            services.AddDbContext<BooksLibraryDbContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("BooksLibraryDbContextConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // adding these for the identity to work
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // adding an endpoint for the route to work
                endpoints.MapRazorPages();
            });
        }
    }
}
