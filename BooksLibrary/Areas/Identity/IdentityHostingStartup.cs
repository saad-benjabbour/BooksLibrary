using System;
using BooksLibrary.Areas.Identity.Data;
using BooksLibrary.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BooksLibrary.Areas.Identity.IdentityHostingStartup))]
namespace BooksLibrary.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BooksLibraryDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BooksLibraryDbContextConnection")));

                services.AddDefaultIdentity<BooksLibraryUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })    
               
                    .AddEntityFrameworkStores<BooksLibraryDbContext>();
            });
        }
    }
}