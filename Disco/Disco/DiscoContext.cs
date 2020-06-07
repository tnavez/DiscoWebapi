using Disco.Entities;
using Disco.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disco
{
    public class DiscoContext : DbContext
    {

        public DiscoContext(DbContextOptions<DiscoContext> options) : base(options)
        {
            Database.Migrate();
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<Cards> Cards { get; set; }
        public DbSet<IdentityCard> IdentityCards { get; set; }
        public DbSet<BlackListLog> BlackListLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
            .HasMany(c => c.Cards)
            .WithOne(m => m.Member)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Member>()
                .HasMany(b => b.BlackListLogs)
                .WithOne(m => m.Member)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Member>()
            .HasOne(a => a.Identity)
            .WithOne(b => b.Member)
            .HasForeignKey<IdentityCard>(b => b.MemberId);
        }
    }
}

