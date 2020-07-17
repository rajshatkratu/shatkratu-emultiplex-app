using EMultiplex.DAL.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EMultiplex.DAL
{
    public class EMultiplexDbContext : IdentityDbContext
    {
        public EMultiplexDbContext(DbContextOptions<EMultiplexDbContext> options) : base(options)
        {

        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Multiplex> Multiplexes { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(x => x.Token);

                entity.Property(x => x.JwtId).IsRequired();
                entity.Property(x => x.UserId).IsRequired();
            });

            builder.Entity<City>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasIndex(x => x.Name).IsUnique();
            });

            builder.Entity<Genre>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasIndex(x => x.Name).IsUnique();
            });


            builder.Entity<Language>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasIndex(x => x.Name).IsUnique();
            });

            builder.Entity<Movie>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();
                entity.HasIndex(x => x.Name).IsUnique();

                entity.HasOne(x => x.Genre)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(x => x.Language)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Multiplex>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).IsRequired();

                entity.HasOne(x => x.City)
                .WithMany(x => x.Multiplexes)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<Show>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.MovieId).IsRequired();
                entity.Property(x => x.MultiplexId).IsRequired();

                entity.HasOne(x => x.Movie)
                .WithMany(x => x.Shows)
                .HasForeignKey(x => x.MovieId);

                entity.HasOne(x => x.Multiplex)
                .WithMany(x => x.Shows)
                .HasForeignKey(x => x.MultiplexId);
            });

            builder.Entity<Reservation>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.ShowId).IsRequired();

                entity.HasOne(x => x.Show)
                      .WithMany(x => x.Reservations)
                      .HasForeignKey(x => x.ShowId)
                      .OnDelete(DeleteBehavior.ClientSetNull);

            });

            base.OnModelCreating(builder);
        }
    }
}
