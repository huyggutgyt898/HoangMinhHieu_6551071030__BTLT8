using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TiemTapHoa.Models;

public partial class TiemTapHoaContext : DbContext
{
    public TiemTapHoaContext()
    {
    }

    public TiemTapHoaContext(DbContextOptions<TiemTapHoaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TiemTapHoa;User Id=sa;Password=SqlServer@123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.MaHang).HasName("PK__HangHoa__19C0DB1D121B3EC8");

            entity.ToTable("HangHoa");

            entity.Property(e => e.DonVi).HasMaxLength(20);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.LoaiHang).HasMaxLength(50);
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            entity.Property(e => e.TenHang).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
