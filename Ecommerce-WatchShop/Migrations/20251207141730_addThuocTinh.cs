using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_WatchShop.Migrations
{
    /// <inheritdoc />
    public partial class addThuocTinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThuocTinhSanPhams",
                columns: table => new
                {
                    MaThuocTinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuocTinh = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(100)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    ThuTuHienThi = table.Column<int>(type: "int", nullable: false),
                    HienThi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinhSanPhams", x => x.MaThuocTinh);
                });

            migrationBuilder.CreateTable(
                name: "GiaTriThuocTinhs",
                columns: table => new
                {
                    MaGiaTri = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaThuocTinh = table.Column<int>(type: "int", nullable: false),
                    GiaTri = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(100)", nullable: true),
                    ThuTuHienThi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTriThuocTinhs", x => x.MaGiaTri);
                    table.ForeignKey(
                        name: "FK_GiaTriThuocTinhs_ThuocTinhSanPhams_MaThuocTinh",
                        column: x => x.MaThuocTinh,
                        principalTable: "ThuocTinhSanPhams",
                        principalColumn: "MaThuocTinh",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamThuocTinhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSanPham = table.Column<int>(type: "int", nullable: false),
                    MaGiaTri = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamThuocTinhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPhamThuocTinhs_GiaTriThuocTinhs_MaGiaTri",
                        column: x => x.MaGiaTri,
                        principalTable: "GiaTriThuocTinhs",
                        principalColumn: "MaGiaTri",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamThuocTinhs_SanPhams_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPhams",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiaTriThuocTinhs_MaThuocTinh",
                table: "GiaTriThuocTinhs",
                column: "MaThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamThuocTinhs_MaGiaTri",
                table: "SanPhamThuocTinhs",
                column: "MaGiaTri");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamThuocTinhs_MaSanPham",
                table: "SanPhamThuocTinhs",
                column: "MaSanPham");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SanPhamThuocTinhs");

            migrationBuilder.DropTable(
                name: "GiaTriThuocTinhs");

            migrationBuilder.DropTable(
                name: "ThuocTinhSanPhams");
        }
    }
}
