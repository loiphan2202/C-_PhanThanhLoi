using CRUD_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRUD_API.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, MethodName = "Theo tháng" },
                new PaymentMethod { Id = 2, MethodName = "Theo quý" },
                new PaymentMethod { Id = 3, MethodName = "Theo năm" }
            );

            modelBuilder.Entity<Room>()
                .ToTable("rooms")
                .HasOne(r => r.PaymentMethod)
                .WithMany(p => p.Rooms)
                .HasForeignKey(r => r.PaymentMethodId);
        }
    }
}
