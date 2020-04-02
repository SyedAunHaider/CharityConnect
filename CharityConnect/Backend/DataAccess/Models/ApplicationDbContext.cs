using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CharityConnect.Backend.DataAccess.Models
{

    public class BaseClass
    {
        public DateTime CreationDate { get; set; }
    }

    public class Proivince: BaseClass
    {
        public int ProvinceId { get; set; }
        public string Name { get; set; }
        public ICollection<City> City { get; set; }
    }

    public class City : BaseClass
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Proivince Proivince { get; set; }
        public ICollection<NAConstituency> NAConstituency { get; set; }
    }

    public class NAConstituency : BaseClass
    {
        public int NAConstituencyId { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<ProvincialConstituency> ProvincialConstituency { get; set; }
    }

    public class ProvincialConstituency : BaseClass
    {
        public int PConstituencyId { get; set; }
        public string Name { get; set; }
        public int NAConstituencyId { get; set; }
        public NAConstituency NAConstituency { get; set; }
        public ICollection<AppUser> AppUser { get; set; }
    }

    public class AppUser : IdentityUser
    {
        // Extended Properties
        public string CNIC { get; set; }
        public string MobileNo { get; set; }
        public int? FamilyMembersCount { get; set; }
        public DateTime? CharityDistributionDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int PConstituencyId { get; set; }
        public ProvincialConstituency ProvincialConstituency { get; set; }
    }

    public class CharityHistory :BaseClass
    {
        public int HistoryId { get; set; }
        public int DonorUserId { get; set; }
        public int RecipientUserId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Proivince> Proivince { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<NAConstituency> NAConstituency { get; set; }
        public DbSet<ProvincialConstituency> ProvincialConstituency { get; set; }
        public DbSet<CharityHistory> CharityHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
                entity.Property(e => e.CharityDistributionDate).HasColumnType("datetime");
                entity.Property(e => e.CNIC).IsRequired();
                entity.Property(e => e.MobileNo).IsRequired();

                entity.HasOne(e => e.ProvincialConstituency)
                  .WithMany(p => p.AppUser)
                  .HasForeignKey(d => d.PConstituencyId)
                  .HasConstraintName("FK_ProvincialConstituencies_AppUsers");
            });

            builder.Entity<CharityHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);
                entity.Property(e => e.HistoryId).IsRequired();
                entity.Property(e => e.HistoryId).ValueGeneratedOnAdd();
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

                builder.Entity<Proivince>(entity =>
            {
                entity.HasKey(e => e.ProvinceId);
                entity.Property(e => e.ProvinceId).IsRequired();
                entity.Property(e => e.ProvinceId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            builder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityId);
                entity.Property(e => e.CityId).IsRequired();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.HasOne(e => e.Proivince)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Proivinces_Cities");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            builder.Entity<NAConstituency>(entity =>
            {
                entity.HasKey(e => e.NAConstituencyId);
                entity.Property(e => e.NAConstituencyId).IsRequired();
                entity.Property(e => e.NAConstituencyId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.HasOne(e => e.City)
                    .WithMany(p => p.NAConstituency)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Cities_NAConstituencies");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            builder.Entity<ProvincialConstituency>(entity =>
            {
                entity.HasKey(e => e.PConstituencyId);
                entity.Property(e => e.PConstituencyId).IsRequired();
                entity.Property(e => e.PConstituencyId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.HasOne(e => e.NAConstituency)
                    .WithMany(p => p.ProvincialConstituency)
                    .HasForeignKey(d => d.NAConstituencyId)
                    .HasConstraintName("FK_NAConstituencies_ProvincialConstituencies");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");
            });

            base.OnModelCreating(builder);
        }
    }
}
