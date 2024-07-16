using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineStore.Helpers;
using OnlineStore.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPaymentMethod> UserPaymentMethods { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cites { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOptions> ProductOptions { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductOptions_Sizes> ProductOptions_Sizes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
            .Entity<User>()
            .Property(e => e.Gender)
            .HasConversion(
            v => v.ToString(),
            v => (Gender)Enum.Parse(typeof(Gender), v));

            builder
            .Entity<Brand>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Brands)Enum.Parse(typeof(Brands), v));

            builder
            .Entity<City>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Cities)Enum.Parse(typeof(Cities), v));

            builder
            .Entity<Country>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Countries)Enum.Parse(typeof(Countries), v));

            builder
            .Entity<Material>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Materials)Enum.Parse(typeof(Materials), v));

            builder
            .Entity<OrderStatus>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Order_Status)Enum.Parse(typeof(Order_Status), v));

            builder
            .Entity<PaymentType>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (PaymentTypes)Enum.Parse(typeof(PaymentTypes), v));

            builder
            .Entity<ProductCategory>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Gender)Enum.Parse(typeof(Gender), v));

            builder
            .Entity<ProductSubCategory>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Categories)Enum.Parse(typeof(Categories), v));

            builder
            .Entity<Season>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Seasons)Enum.Parse(typeof(Seasons), v));

            builder
            .Entity<ShippingMethod>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (ShippingMethods)Enum.Parse(typeof(ShippingMethods), v));

            builder
            .Entity<Style>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Styles)Enum.Parse(typeof(Styles), v));

            builder
            .Entity<Size>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Sizes)Enum.Parse(typeof(Sizes), v));

            builder
            .Entity<Color>()
            .Property(e => e.Name)
            .HasConversion(
            v => v.ToString(),
            v => (Colors)Enum.Parse(typeof(Colors), v));

            builder.Entity<Order>()
                .HasOne(o => o.UserPaymentMethod)
                .WithMany(u => u.Orders).
                HasForeignKey(oi => oi.UserPaymentMethodId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders).
                HasForeignKey(oi => oi.UserId)
               .OnDelete(DeleteBehavior.Restrict);
           
       
            
            builder.Entity<Product>()
                .HasOne(o => o.Category)
                .WithMany(c => c.Products).
                HasForeignKey(oi => oi.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(po => po.SubCategory)
                .WithMany(c => c.Products).
                HasForeignKey(po => po.SubCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(po => po.Material)
                .WithMany(c => c.Products).
                HasForeignKey(po => po.MaterialId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(po => po.Season)
                .WithMany(c => c.Products).
                HasForeignKey(po => po.SeasonId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductOptions>()
                .HasOne(po => po.Color)
                .WithMany(c => c.ProductOptions).
                HasForeignKey(po => po.ColorId)
               .OnDelete(DeleteBehavior.Restrict);

            //builder.Seed();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is BaseEntity && (
                                    e.State == EntityState.Added
                                    || e.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                ((BaseEntity)entity.Entity).UpdatedOn = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedOn = DateTime.Now;
                }
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}
