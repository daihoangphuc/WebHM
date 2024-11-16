using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebHM.Models;

namespace WebHM.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Khai báo DbSet cho các thực thể trong ứng dụng
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<BinhLuan> BinhLuans { get; set; }
        public DbSet<ThongKeDoanhSo> ThongKeDoanhSos { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<LichSuThanhToan> LichSuThanhToans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Cấu hình khóa ngoại cho các bảng
            builder.Entity<SanPham>()
                .HasOne(s => s.NguoiBan)
                .WithMany()
                .HasForeignKey(s => s.NguoiBanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SanPham>()
                .HasOne(s => s.DanhMuc)
                .WithMany(d => d.SanPhams)
                .HasForeignKey(s => s.MaDanhMuc);

            builder.Entity<DonHang>()
                .HasOne(d => d.KhachHang)
                .WithMany()
                .HasForeignKey(d => d.KhachHangId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChiTietDonHang>()
                .HasOne(c => c.DonHang)
                .WithMany(d => d.ChiTietDonHangs)
                .HasForeignKey(c => c.MaDonHang);

            builder.Entity<ChiTietDonHang>()
                .HasOne(c => c.SanPham)
                .WithMany(s => s.ChiTietDonHangs)
                .HasForeignKey(c => c.MaSanPham);

            builder.Entity<DanhGia>()
                .HasOne(r => r.SanPham)
                .WithMany(s => s.DanhGias)
                .HasForeignKey(r => r.MaSanPham);

            builder.Entity<DanhGia>()
                .HasOne(r => r.KhachHang)
                .WithMany()
                .HasForeignKey(r => r.KhachHangId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BinhLuan>()
                .HasOne(b => b.SanPham)
                .WithMany(s => s.BinhLuans)
                .HasForeignKey(b => b.MaSanPham);

            builder.Entity<BinhLuan>()
                .HasOne(b => b.KhachHang)
                .WithMany()
                .HasForeignKey(b => b.KhachHangId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GioHang>()
                .HasOne(g => g.KhachHang)
                .WithMany()
                .HasForeignKey(g => g.KhachHangId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<GioHang>()
                .HasOne(g => g.SanPham)
                .WithMany(s => s.GioHangs)
                .HasForeignKey(g => g.MaSanPham);

            builder.Entity<ThongKeDoanhSo>()
                .HasOne(t => t.NguoiBan)
                .WithMany()
                .HasForeignKey(t => t.NguoiBanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ThongKeDoanhSo>()
                .HasOne(t => t.SanPham)
                .WithMany(s => s.ThongKeDoanhSos)
                .HasForeignKey(t => t.MaSanPham);

            builder.Entity<ThanhToan>()
                .HasOne(t => t.KhachHang)
                .WithMany()
                .HasForeignKey(t => t.KhachHangId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LichSuThanhToan>()
                .HasOne(l => l.ThanhToan)
                .WithMany()
                .HasForeignKey(l => l.MaThanhToan);
        }
    }
}
