using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_WatchShop.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanSanPhams_SanPhams_SanPhamMaSanPham",
                table: "BinhLuanSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGiaSanPhams_SanPhams_SanPhamMaSanPham",
                table: "DanhGiaSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_DanhGiaSanPhams_SanPhamMaSanPham",
                table: "DanhGiaSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_BinhLuanSanPhams_SanPhamMaSanPham",
                table: "BinhLuanSanPhams");

            migrationBuilder.DropColumn(
                name: "SanPhamMaSanPham",
                table: "DanhGiaSanPhams");

            migrationBuilder.DropColumn(
                name: "SanPhamMaSanPham",
                table: "BinhLuanSanPhams");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaSanPhams_MaKhachHang",
                table: "DanhGiaSanPhams",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaSanPhams_MaSanPham",
                table: "DanhGiaSanPhams",
                column: "MaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanSanPhams_MaKhachHang",
                table: "BinhLuanSanPhams",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanSanPhams_MaSanPham",
                table: "BinhLuanSanPhams",
                column: "MaSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanSanPhams_KhachHangs_MaKhachHang",
                table: "BinhLuanSanPhams",
                column: "MaKhachHang",
                principalTable: "KhachHangs",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanSanPhams_SanPhams_MaSanPham",
                table: "BinhLuanSanPhams",
                column: "MaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGiaSanPhams_KhachHangs_MaKhachHang",
                table: "DanhGiaSanPhams",
                column: "MaKhachHang",
                principalTable: "KhachHangs",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGiaSanPhams_SanPhams_MaSanPham",
                table: "DanhGiaSanPhams",
                column: "MaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanSanPhams_KhachHangs_MaKhachHang",
                table: "BinhLuanSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_BinhLuanSanPhams_SanPhams_MaSanPham",
                table: "BinhLuanSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGiaSanPhams_KhachHangs_MaKhachHang",
                table: "DanhGiaSanPhams");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGiaSanPhams_SanPhams_MaSanPham",
                table: "DanhGiaSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_DanhGiaSanPhams_MaKhachHang",
                table: "DanhGiaSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_DanhGiaSanPhams_MaSanPham",
                table: "DanhGiaSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_BinhLuanSanPhams_MaKhachHang",
                table: "BinhLuanSanPhams");

            migrationBuilder.DropIndex(
                name: "IX_BinhLuanSanPhams_MaSanPham",
                table: "BinhLuanSanPhams");

            migrationBuilder.AddColumn<int>(
                name: "SanPhamMaSanPham",
                table: "DanhGiaSanPhams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SanPhamMaSanPham",
                table: "BinhLuanSanPhams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaSanPhams_SanPhamMaSanPham",
                table: "DanhGiaSanPhams",
                column: "SanPhamMaSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanSanPhams_SanPhamMaSanPham",
                table: "BinhLuanSanPhams",
                column: "SanPhamMaSanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_BinhLuanSanPhams_SanPhams_SanPhamMaSanPham",
                table: "BinhLuanSanPhams",
                column: "SanPhamMaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGiaSanPhams_SanPhams_SanPhamMaSanPham",
                table: "DanhGiaSanPhams",
                column: "SanPhamMaSanPham",
                principalTable: "SanPhams",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
