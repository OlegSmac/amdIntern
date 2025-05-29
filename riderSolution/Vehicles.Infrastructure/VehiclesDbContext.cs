using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Infrastructure;

public class VehiclesDbContext : DbContext
{
    public VehiclesDbContext(DbContextOptions<VehiclesDbContext> options) : base(options) { }
    
    //Vehicles
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Car> Cars { get; set; } = default!;
    public DbSet<Motorcycle> Motorcycles { get; set; } = default!;
    public DbSet<Truck> Trucks { get; set; } = default!;
    
    //Users
    public DbSet<RegularUser> RegularUsers { get; set; } = default!;
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<Subscription> Subscriptions { get; set; } = default!;
    
    //Posts
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<FavoritePost> FavoritePosts { get; set; } = default!;
    
    //Notifications
    public DbSet<CompanyNotification> CompanyNotifications { get; set; } = default!;
    public DbSet<UserNotification> UserNotifications { get; set; } = default!;

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=Vehicles;User Id=sa;Password=olegandilie100S&;TrustServerCertificate=true");
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Vehicles description
        modelBuilder.Entity<Vehicle>().ToTable("Vehicles").UseTptMappingStrategy();
        
        //Users description
        modelBuilder.Entity<RegularUser>();
        modelBuilder.Entity<Company>();
        modelBuilder.Entity<Admin>();
        
        //Posts description
        modelBuilder.Entity<Post>();
        modelBuilder.Entity<Category>();
        
        //Favorite list description
        modelBuilder.Entity<FavoritePost>().HasKey(fl => new { fl.PostId, fl.UserId });
        modelBuilder.Entity<FavoritePost>()
            .HasOne(fl => fl.Post)
            .WithMany(p => p.FavoritePosts)
            .HasForeignKey(fl => fl.PostId);
        modelBuilder.Entity<FavoritePost>()
            .HasOne(fl => fl.User)
            .WithMany(u => u.FavoritePosts)
            .HasForeignKey(fl => fl.UserId);
        
        //Company notification description
        modelBuilder.Entity<CompanyNotification>()
            .HasOne(cn => cn.Company)
            .WithMany(c => c.CompanyNotifications) 
            .HasForeignKey(cn => cn.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<CompanyNotification>()
            .HasOne(cn => cn.Post)
            .WithMany()
            .HasForeignKey(cn => cn.PostId)
            .OnDelete(DeleteBehavior.NoAction);
        
        //User notification description
        modelBuilder.Entity<UserNotification>()
            .HasOne(un => un.User)
            .WithMany(u => u.UserNotifications)
            .HasForeignKey(un => un.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<UserNotification>()
            .HasOne(un => un.Post)
            .WithMany()
            .HasForeignKey(un => un.PostId)
            .OnDelete(DeleteBehavior.NoAction);
        
        //Subscription description
        modelBuilder.Entity<Subscription>().HasKey(s => new { s.UserId, s.CompanyId });
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Company)
            .WithMany(c => c.Subscribers)
            .HasForeignKey(s => s.CompanyId);
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.User)
            .WithMany(u => u.Subscribers)
            .HasForeignKey(s => s.UserId);


    }
}