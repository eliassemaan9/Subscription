using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace subscription.models.Models;

public partial class SubscriptionContext : DbContext
{
    public SubscriptionContext()
    {
    }

    public SubscriptionContext(DbContextOptions<SubscriptionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountDetail> DiscountDetails { get; set; }

    public virtual DbSet<Lookup> Lookups { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=subscription;Username=postgres;Password=P@ssw0rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pk");

            entity.ToTable("Address", "subscription");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.PostalCode).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("address_fk");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("discount_pk");

            entity.ToTable("Discount", "subscription");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.PromotionCode).HasColumnType("character varying");
            entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("discount_fk_1");

            entity.HasOne(d => d.User).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("discount_fk");
        });

        modelBuilder.Entity<DiscountDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("discountdetails_pk");

            entity.ToTable("DiscountDetails", "subscription");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.RedeemDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Discount).WithMany(p => p.DiscountDetails)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("discountdetails_fk");
        });

        modelBuilder.Entity<Lookup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Lookups_pkey");

            entity.ToTable("Lookups", "subscription");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreationDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.LookupCode).HasMaxLength(500);
            entity.Property(e => e.ModificationDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("price_pk");

            entity.ToTable("Price", "subscription");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Price1).HasColumnName("Price");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Prices)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("price_fk");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscription_pkey");

            entity.ToTable("Subscription", "subscription");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CancelAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CanceledAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.EndedAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("subscription_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.ToTable("Users", "subscription");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(256);
            entity.Property(e => e.Salt).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
