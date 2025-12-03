using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_WatchShop.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaiViets",
                columns: table => new
                {
                    MaBaiViet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhAnh = table.Column<string>(type: "varchar(255)", nullable: true),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiViets", x => x.MaBaiViet);
                });

            migrationBuilder.CreateTable(
                name: "ChinhSachs",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChinhSachs", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MaDanhMucCha = table.Column<int>(type: "int", nullable: true),
                    Slug = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.MaDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "FooterLinks",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNhom = table.Column<int>(type: "int", nullable: false),
                    ThuTuHienThi = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterLinks", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "Footers",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footers", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "GioiThieus",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioiThieus", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "LienHes",
                columns: table => new
                {
                    MaLienHe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TieuDe = table.Column<string>(type: "varchar(200)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LienHes", x => x.MaLienHe);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Ma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TieuDe = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThuTuHienThi = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    HienThiTrangChu = table.Column<bool>(type: "bit", nullable: false),
                    HienThiTrangSanPham = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Ma);
                });

            migrationBuilder.CreateTable(
                name: "Tinhs",
                columns: table => new
                {
                    MaTinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTinh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tinhs", x => x.MaTinh);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    MaVaiTro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Loai = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.MaVaiTro);
                });

            migrationBuilder.CreateTable(
                name: "HinhAnhBaiViets",
                columns: table => new
                {
                    MaHinhAnhBaiViet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBaiViet = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaiVietMaBaiViet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnhBaiViets", x => x.MaHinhAnhBaiViet);
                    table.ForeignKey(
                        name: "FK_HinhAnhBaiViets_BaiViets_BaiVietMaBaiViet",
                        column: x => x.BaiVietMaBaiViet,
                        principalTable: "BaiViets",
                        principalColumn: "MaBaiViet");
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieus",
                columns: table => new
                {
                    MaThuongHieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Slug = table.Column<string>(type: "varchar(100)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieus", x => x.MaThuongHieu);
                    table.ForeignKey(
                        name: "FK_ThuongHieus_DanhMucs_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "MaDanhMuc");
                });

            migrationBuilder.CreateTable(
                name: "Huyens",
                columns: table => new
                {
                    MaHuyen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaTinh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huyens", x => x.MaHuyen);
                    table.ForeignKey(
                        name: "FK_Huyens_Tinhs_MaTinh",
                        column: x => x.MaTinh,
                        principalTable: "Tinhs",
                        principalColumn: "MaTinh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoans",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "varchar(100)", nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(100)", nullable: true),
                    MaVaiTro = table.Column<int>(type: "int", nullable: true),
                    VaiTroMaVaiTro = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoans", x => x.MaTaiKhoan);
                    table.ForeignKey(
                        name: "FK_TaiKhoans_VaiTros_VaiTroMaVaiTro",
                        column: x => x.VaiTroMaVaiTro,
                        principalTable: "VaiTros",
                        principalColumn: "MaVaiTro");
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    MaSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhAnh = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    MaThuongHieu = table.Column<int>(type: "int", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    GiaKhuyenMai = table.Column<double>(type: "float", nullable: true),
                    PhanTramKhuyenMai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MoTaNgan = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ThongSoKyThuat = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    LuotXem = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaXoa = table.Column<int>(type: "int", nullable: true),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.MaSanPham);
                    table.ForeignKey(
                        name: "FK_SanPhams_DanhMucs_MaDanhMuc",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMucs",
                        principalColumn: "MaDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhams_ThuongHieus_MaThuongHieu",
                        column: x => x.MaThuongHieu,
                        principalTable: "ThuongHieus",
                        principalColumn: "MaThuongHieu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Xas",
                columns: table => new
                {
                    MaXa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenXa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaHuyen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xas", x => x.MaXa);
                    table.ForeignKey(
                        name: "FK_Xas_Huyens_MaHuyen",
                        column: x => x.MaHuyen,
                        principalTable: "Huyens",
                        principalColumn: "MaHuyen",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenKhoiPhucMatKhaus",
                columns: table => new
                {
                    MaDinhDanh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false),
                    MaToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenKhoiPhucMatKhaus", x => x.MaDinhDanh);
                    table.ForeignKey(
                        name: "FK_TokenKhoiPhucMatKhaus_TaiKhoans_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "MaTaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HinhAnhSanPhams",
                columns: table => new
                {
                    MaHinhAnhSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSanPham = table.Column<int>(type: "int", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    SanPhamMaSanPham = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnhSanPhams", x => x.MaHinhAnhSanPham);
                    table.ForeignKey(
                        name: "FK_HinhAnhSanPhams_SanPhams_SanPhamMaSanPham",
                        column: x => x.SanPhamMaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham");
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "varchar(15)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    TenHienThi = table.Column<string>(type: "varchar(200)", nullable: true),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    MaTinh = table.Column<int>(type: "int", nullable: true),
                    MaHuyen = table.Column<int>(type: "int", nullable: true),
                    MaXa = table.Column<int>(type: "int", nullable: true),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKhachHang);
                    table.ForeignKey(
                        name: "FK_KhachHangs_Huyens_MaHuyen",
                        column: x => x.MaHuyen,
                        principalTable: "Huyens",
                        principalColumn: "MaHuyen",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KhachHangs_TaiKhoans_MaTaiKhoan",
                        column: x => x.MaTaiKhoan,
                        principalTable: "TaiKhoans",
                        principalColumn: "MaTaiKhoan");
                    table.ForeignKey(
                        name: "FK_KhachHangs_Tinhs_MaTinh",
                        column: x => x.MaTinh,
                        principalTable: "Tinhs",
                        principalColumn: "MaTinh",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_KhachHangs_Xas_MaXa",
                        column: x => x.MaXa,
                        principalTable: "Xas",
                        principalColumn: "MaXa",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanSanPhams",
                columns: table => new
                {
                    MaBinhLuan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KhachHangMaKhachHang = table.Column<int>(type: "int", nullable: true),
                    SanPhamMaSanPham = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanSanPhams", x => x.MaBinhLuan);
                    table.ForeignKey(
                        name: "FK_BinhLuanSanPhams_KhachHangs_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang");
                    table.ForeignKey(
                        name: "FK_BinhLuanSanPhams_SanPhams_SanPhamMaSanPham",
                        column: x => x.SanPhamMaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGiaSanPhams",
                columns: table => new
                {
                    MaDanhGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: true),
                    DiemDanhGia = table.Column<int>(type: "int", nullable: true),
                    KhachHangMaKhachHang = table.Column<int>(type: "int", nullable: true),
                    SanPhamMaSanPham = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGiaSanPhams", x => x.MaDanhGia);
                    table.ForeignKey(
                        name: "FK_DanhGiaSanPhams_KhachHangs_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang");
                    table.ForeignKey(
                        name: "FK_DanhGiaSanPhams_SanPhams_SanPhamMaSanPham",
                        column: x => x.SanPhamMaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    NgayDatHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "varchar(15)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Tinh = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Huyen = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Xa = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TongTien = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    YeuCauHuy = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    NgayYeuCauHuy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaYeuCauHuy = table.Column<bool>(type: "bit", nullable: false),
                    KhachHangMaKhachHang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDons_KhachHangs_KhachHangMaKhachHang",
                        column: x => x.KhachHangMaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang");
                });

            migrationBuilder.CreateTable(
                name: "YeuThichs",
                columns: table => new
                {
                    MaYeuThich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuThichs", x => x.MaYeuThich);
                    table.ForeignKey(
                        name: "FK_YeuThichs_KhachHangs_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_YeuThichs_SanPhams_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDons",
                columns: table => new
                {
                    MaChiTietHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,0)", precision: 18, scale: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDons", x => x.MaChiTietHoaDon);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_HoaDons_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "HoaDons",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_SanPhams_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanSanPhams_KhachHangMaKhachHang",
                table: "BinhLuanSanPhams",
                column: "KhachHangMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanSanPhams_SanPhamMaSanPham",
                table: "BinhLuanSanPhams",
                column: "SanPhamMaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MaHoaDon",
                table: "ChiTietHoaDons",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MaSanPham",
                table: "ChiTietHoaDons",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaSanPhams_KhachHangMaKhachHang",
                table: "DanhGiaSanPhams",
                column: "KhachHangMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaSanPhams_SanPhamMaSanPham",
                table: "DanhGiaSanPhams",
                column: "SanPhamMaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnhBaiViets_BaiVietMaBaiViet",
                table: "HinhAnhBaiViets",
                column: "BaiVietMaBaiViet");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnhSanPhams_SanPhamMaSanPham",
                table: "HinhAnhSanPhams",
                column: "SanPhamMaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_KhachHangMaKhachHang",
                table: "HoaDons",
                column: "KhachHangMaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_Huyens_MaTinh",
                table: "Huyens",
                column: "MaTinh");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaHuyen",
                table: "KhachHangs",
                column: "MaHuyen");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaTaiKhoan",
                table: "KhachHangs",
                column: "MaTaiKhoan",
                unique: true,
                filter: "[MaTaiKhoan] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaTinh",
                table: "KhachHangs",
                column: "MaTinh");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_MaXa",
                table: "KhachHangs",
                column: "MaXa");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaDanhMuc",
                table: "SanPhams",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhams_MaThuongHieu",
                table: "SanPhams",
                column: "MaThuongHieu");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoans_VaiTroMaVaiTro",
                table: "TaiKhoans",
                column: "VaiTroMaVaiTro");

            migrationBuilder.CreateIndex(
                name: "IX_ThuongHieus_MaDanhMuc",
                table: "ThuongHieus",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_TokenKhoiPhucMatKhaus_MaTaiKhoan",
                table: "TokenKhoiPhucMatKhaus",
                column: "MaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_Xas_MaHuyen",
                table: "Xas",
                column: "MaHuyen");

            migrationBuilder.CreateIndex(
                name: "IX_YeuThichs_MaKhachHang",
                table: "YeuThichs",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_YeuThichs_MaSanPham",
                table: "YeuThichs",
                column: "MaSanPham");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinhLuanSanPhams");

            migrationBuilder.DropTable(
                name: "ChinhSachs");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDons");

            migrationBuilder.DropTable(
                name: "DanhGiaSanPhams");

            migrationBuilder.DropTable(
                name: "FooterLinks");

            migrationBuilder.DropTable(
                name: "Footers");

            migrationBuilder.DropTable(
                name: "GioiThieus");

            migrationBuilder.DropTable(
                name: "HinhAnhBaiViets");

            migrationBuilder.DropTable(
                name: "HinhAnhSanPhams");

            migrationBuilder.DropTable(
                name: "LienHes");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "TokenKhoiPhucMatKhaus");

            migrationBuilder.DropTable(
                name: "YeuThichs");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "BaiViets");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "ThuongHieus");

            migrationBuilder.DropTable(
                name: "TaiKhoans");

            migrationBuilder.DropTable(
                name: "Xas");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "VaiTros");

            migrationBuilder.DropTable(
                name: "Huyens");

            migrationBuilder.DropTable(
                name: "Tinhs");
        }
    }
}
