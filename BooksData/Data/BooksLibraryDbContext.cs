using BooksLibrary.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BooksData.Models;
namespace BooksLibrary.Data
{
    public class BooksLibraryDbContext : IdentityDbContext<BooksLibraryUser>
    {
        public BooksLibraryDbContext(DbContextOptions<BooksLibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<BooksLibraryUser>(entity => { entity.ToTable(name: "Patrons"); });
        }
        // the tables of our DB
        public DbSet<BooksLibraryUser> BooksLibraryUser { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<LibraryAsset> libraryAssets { get; set; }
        public DbSet<LibraryBranch> libraryBranchs { get; set; }
        public DbSet<LibraryCard> libraryCards { get; set; }

        public DbSet<Status> Status { get; set; }
        public DbSet<Video> videos { get; set; }
        public DbSet<Hold> holds { get; set; }

        public DbSet<AssetType> assetTypes { get; set; }
        public DbSet<CheckoutHistory> checkoutHistories { get; set; }
        public DbSet<BranchHours> branchHours { get; set; }
    }
}
