using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppMvc.Models;

public partial class CustomerLogInfoContext : DbContext
{
    public CustomerLogInfoContext()
    {
    }

    public CustomerLogInfoContext(DbContextOptions<CustomerLogInfoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustLogInfo> CustLogInfos { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server = DESKTOP-FG5F3C3; database = CustomerLogInfo; trusted_Connection = true; TrustServerCertificate = true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustLogInfo>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__CustLogI__5E54864895D85963");

            entity.ToTable("CustLogInfo");

            entity.Property(e => e.LogId).ValueGeneratedNever();
            entity.Property(e => e.CustEmail).HasMaxLength(50);
            entity.Property(e => e.CustName).HasMaxLength(50);
            entity.Property(e => e.LogStatus).HasMaxLength(50);

            entity.HasOne(d => d.UserInfoNavigation).WithMany(p => p.CustLogInfos)
                .HasForeignKey(d => d.UserInfo)
                .HasConstraintName("FK__CustLogIn__UserI__3A81B327");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserInfo__1788CC4CB94F6740");

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
