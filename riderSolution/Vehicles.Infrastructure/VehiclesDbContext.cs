using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;
using Vehicles.Domain.VehicleTypes.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Infrastructure;

public class VehiclesDbContext : IdentityDbContext<ApplicationUser>
{
    public VehiclesDbContext(DbContextOptions<VehiclesDbContext> options) : base(options) { }
    
    //Vehicles
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Car> Cars { get; set; } = default!;
    public DbSet<Motorcycle> Motorcycles { get; set; } = default!;
    public DbSet<Truck> Trucks { get; set; } = default!;
    
    //Users
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<Subscription> Subscriptions { get; set; } = default!;
    
    //Posts
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<PostImage> PostImages { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<FavoritePost> FavoritePosts { get; set; } = default!;
    
    //Notifications
    public DbSet<Notification> Notifications { get; set; } = default!;
    public DbSet<UserNotification> UserNotifications { get; set; } = default!;
    public DbSet<CompanyNotification> CompanyNotifications { get; set; } = default!;
    public DbSet<AdminNotification> AdminNotifications { get; set; } = default!;
    
    //Vehicle models
    public DbSet<Brand> Brands { get; set; } = default!;
    public DbSet<Model> Models { get; set; } = default!;
    public DbSet<Year> Years { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Vehicles description
        modelBuilder.Entity<Vehicle>().ToTable("Vehicles").UseTptMappingStrategy();
        
        //Users description
        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.Id)
            .HasMaxLength(100);
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Company>();
        modelBuilder.Entity<Admin>();
        
        //Posts description
        modelBuilder.Entity<Post>()
            .Navigation(p => p.Categories)
            .AutoInclude();
        modelBuilder.Entity<Category>();
        
        //Favorite list description
        modelBuilder.Entity<FavoritePost>().HasKey(fl => new { fl.PostId, fl.UserId });
        modelBuilder.Entity<FavoritePost>()
            .HasOne(fl => fl.Post)
            .WithMany(p => p.FavoritePosts)
            .HasForeignKey(fl => fl.PostId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<FavoritePost>()
            .HasOne(fl => fl.User)
            .WithMany(u => u.FavoritePosts)
            .HasForeignKey(fl => fl.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //Notification description
        modelBuilder.Entity<Notification>()
            .HasDiscriminator<string>("NotificationType")
            .HasValue<UserNotification>("User")
            .HasValue<CompanyNotification>("Company")
            .HasValue<AdminNotification>("Admin");
        
        modelBuilder.Entity<UserNotification>()
            .HasOne(un => un.User)
            .WithMany(u => u.UserNotifications)
            .HasForeignKey(un => un.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CompanyNotification>()
            .HasOne(cn => cn.Company)
            .WithMany(c => c.CompanyNotifications)
            .HasForeignKey(cn => cn.CompanyId)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<AdminNotification>()
            .HasOne(an => an.Admin)
            .WithMany(a => a.AdminNotifications)
            .HasForeignKey(an => an.AdminId)
            .OnDelete(DeleteBehavior.NoAction);
        
        //Subscription description
        modelBuilder.Entity<Subscription>().HasKey(s => new { s.UserId, s.CompanyId });
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.User)
            .WithMany(u => u.Subscribers)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Company)
            .WithMany(c => c.Subscribers)
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        //Vehicle models
        modelBuilder.Entity<Year>().ToTable("ModelYears");
    }
}