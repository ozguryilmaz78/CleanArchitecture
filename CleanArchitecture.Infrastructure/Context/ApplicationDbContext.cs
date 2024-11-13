using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CleanArchitecture.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  // Base class konfigurasyonunu çağır

            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();

            // AppRole ve AppUser yapılandırmaları
            builder.Entity<AppRole>()
                .ToTable("AppRoles")
                .Property(r => r.Description)
                .IsRequired();

            builder.Entity<AppUser>()
                .ToTable("AppUsers")
                .Property(u => u.FullName)
                .IsRequired();

            // AppUserRole için birleşik anahtar ve ilişkiler
            builder.Entity<IdentityUserRole<Guid>>() // AppUserRole yerine IdentityUserRole<Guid> kullanıyoruz
                .ToTable("AppUserRoles")
                .HasKey(ur => new { ur.UserId, ur.RoleId }); // Birleşik anahtar

            // UserRole ilişkilerini tanımlıyoruz
            builder.Entity<IdentityUserRole<Guid>>() // IdentityUserRole<Guid> kullanıyoruz
                .HasOne<AppUser>()
                .WithMany(u => u.UserRoles) // AppUser'da UserRoles özelliği tanımlanmalı
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<IdentityUserRole<Guid>>()
                .HasOne<AppRole>()
                .WithMany(r => r.UserRoles) // AppRole'de UserRoles özelliği tanımlanmalı
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }

        // AppUserRole yerine IdentityUserRole<Guid> tanımlandığından UserRoles DbSet'i kaldırıldı
    }
}
