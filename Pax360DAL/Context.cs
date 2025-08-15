using Azure;
using Microsoft.EntityFrameworkCore;
using Pax360DAL.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection.Emit;

namespace Pax360DAL
{
    public class Context : DbContext, IDisposable
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<RoleTypes> RoleTypes { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Authorizations> Authorizations { get; set; }
        public DbSet<Offers> Offers { get; set; }
        public DbSet<OffersItem> OffersItem { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersItem> OrdersItem { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offers>()
              .HasMany(c => c.OfferItems)
              .WithOne(e => e.Offer)
              .HasForeignKey(s => s.OfferID);

            modelBuilder.Entity<Orders>()
             .HasMany(c => c.OrderItems)
             .WithOne(e => e.Order)
             .HasForeignKey(s => s.OrderID);
        }
    }
}
