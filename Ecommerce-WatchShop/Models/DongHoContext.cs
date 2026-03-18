using Microsoft.EntityFrameworkCore;

namespace Ecommerce_WatchShop.Models;

public partial class DongHoContext : DbContext
{
    public DongHoContext(DbContextOptions<DongHoContext> options) : base(options)
    {
    }
    public required virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public required virtual DbSet<HoaDon> HoaDons { get; set; }

    public required virtual DbSet<BaiViet> BaiViets { get; set; }

    public required virtual DbSet<HinhAnhBaiViet> HinhAnhBaiViets { get; set; }

    public required virtual DbSet<ThuongHieu> ThuongHieus { get; set; }

    public required virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public required virtual DbSet<LienHe> LienHes { get; set; }

    public required virtual DbSet<KhachHang> KhachHangs { get; set; }

    public DbSet<TokenKhoiPhucMatKhau> TokenKhoiPhucMatKhaus { get; set; }

    public required virtual DbSet<YeuThich> YeuThichs { get; set; }

    public required virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public required virtual DbSet<SanPham> SanPhams { get; set; }

    public required virtual DbSet<Tinh> Tinhs { get; set; }

    public required virtual DbSet<Huyen> Huyens { get; set; }

    public required virtual DbSet<Xa> Xas { get; set; }

    public required virtual DbSet<BinhLuanSanPham> BinhLuanSanPhams { get; set; }

    public required virtual DbSet<HinhAnhSanPham> HinhAnhSanPhams { get; set; }

    public required virtual DbSet<DanhGiaSanPham> DanhGiaSanPhams { get; set; }

    public required virtual DbSet<VaiTro> VaiTros { get; set; }

    public required virtual DbSet<Footer> Footers { get; set; }

    public required virtual DbSet<FooterLink> FooterLinks { get; set; }

    public required virtual DbSet<GioiThieu> GioiThieus { get; set; }

    public required virtual DbSet<ChinhSach> ChinhSachs { get; set; }

    public required virtual DbSet<Slider> Sliders { get; set; }

    public required virtual DbSet<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; }

    public required virtual DbSet<GiaTriThuocTinh> GiaTriThuocTinhs { get; set; }

    public required virtual DbSet<SanPhamThuocTinh> SanPhamThuocTinhs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.Property(e => e.MaSanPham).ValueGeneratedOnAdd();
            entity.Property(e => e.Gia).HasColumnType("float"); // Điều chỉnh để khớp database nếu cần
            entity.HasOne(s => s.ThuongHieu)
                  .WithMany(t => t.SanPhams)
                  .HasForeignKey(s => s.MaThuongHieu);
            entity.HasOne(s => s.DanhMuc)
                  .WithMany(d => d.SanPhams)
                  .HasForeignKey(s => s.MaDanhMuc);
        });

        // Cấu hình cho ThuongHieu và DanhMuc nếu cần
        modelBuilder.Entity<ThuongHieu>(entity =>
        {
            entity.Property(e => e.MaThuongHieu).ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.Property(e => e.MaDanhMuc).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<YeuThich>(entity =>
        {
            entity.HasKey(yt => yt.MaYeuThich);
            entity.HasOne(yt => yt.SanPham)
                  .WithMany(p => p.YeuThichs)
                  .HasForeignKey(yt => yt.MaSanPham)
                  .OnDelete(DeleteBehavior.Cascade); // Hoặc NoAction tùy theo yêu cầu
            entity.HasOne(yt => yt.KhachHang)
                  .WithMany(k => k.YeuThichs)
                  .HasForeignKey(yt => yt.MaKhachHang)
                  .OnDelete(DeleteBehavior.SetNull); // Cho phép xóa khách hàng mà không xóa YeuThich
        });

        // Cấu hình mối quan hệ 1-1 giữa TaiKhoan và KhachHang
        modelBuilder.Entity<TaiKhoan>()
            .HasOne(t => t.KhachHang)
            .WithOne(k => k.TaiKhoan)
            .HasForeignKey<KhachHang>(k => k.MaTaiKhoan);

        // Đảm bảo khóa chính và khóa ngoại khớp
        modelBuilder.Entity<KhachHang>()
            .HasKey(k => k.MaKhachHang);

        // Thiết lập mối quan hệ cho bảng địa chỉ
        modelBuilder.Entity<Huyen>()
            .HasOne(h => h.Tinh)
            .WithMany(t => t.Huyens)
            .HasForeignKey(h => h.MaTinh);

        modelBuilder.Entity<Xa>()
            .HasOne(x => x.Huyen)
            .WithMany(h => h.Xas)
            .HasForeignKey(x => x.MaHuyen);

        // Thiết lập mối quan hệ cho KhachHang với địa chỉ
        modelBuilder.Entity<KhachHang>()
            .HasOne(k => k.Tinh)
            .WithMany()
            .HasForeignKey(k => k.MaTinh)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<KhachHang>()
            .HasOne(k => k.Huyen)
            .WithMany()
            .HasForeignKey(k => k.MaHuyen)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<KhachHang>()
            .HasOne(k => k.Xa)
            .WithMany()
            .HasForeignKey(k => k.MaXa)
            .OnDelete(DeleteBehavior.SetNull);

        // Cấu hình cho BinhLuanSanPham
        modelBuilder.Entity<BinhLuanSanPham>(entity =>
        {
            entity.HasKey(b => b.MaBinhLuan);
            entity.HasOne(b => b.SanPham)
                  .WithMany(p => p.BinhLuanSanPhams)
                  .HasForeignKey(b => b.MaSanPham)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(b => b.KhachHang)
                  .WithMany()
                  .HasForeignKey(b => b.MaKhachHang)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Cấu hình cho DanhGiaSanPham
        modelBuilder.Entity<DanhGiaSanPham>(entity =>
        {
            entity.HasKey(d => d.MaDanhGia);
            entity.HasOne(d => d.SanPham)
                  .WithMany(p => p.DanhGiaSanPhams)
                  .HasForeignKey(d => d.MaSanPham)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.KhachHang)
                  .WithMany()
                  .HasForeignKey(d => d.MaKhachHang)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
