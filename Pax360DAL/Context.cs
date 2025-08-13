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
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
