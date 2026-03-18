using Ecommerce_WatchShop.Models;
using Ecommerce_WatchShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_WatchShop.Controllers
{

    public class ProductController : Controller
    {

        private readonly DongHoContext _context;
        public ProductController(DongHoContext context)
        {

            _context = context;
        }

        public async Task<IActionResult> Index(string? search, string? categories = "", string? brands = "", double? minPrice = null, double? maxPrice = null, int page = 1, List<int>? attributeValues = null)
        {
            var pageSize = 5;
            var products = _context.SanPhams
                .Include(p => p.SanPhamThuocTinhs)
                .AsQueryable();

            // Lọc theo tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower().Trim();
                products = products.Where(p =>
                    p.TenSanPham != null && p.TenSanPham.ToLower().Contains(search) ||
                    p.MoTaNgan != null && p.MoTaNgan.ToLower().Contains(search));
            }

            // Lọc theo danh mục (xử lý khi Slug là NULL)
            if (!string.IsNullOrEmpty(categories))
            {
                products = products.Where(p => p.DanhMuc != null && p.DanhMuc.Slug == categories);
            }

            // Lọc theo thương hiệu (xử lý khi Slug là NULL)
            if (!string.IsNullOrEmpty(brands))
            {
                products = products.Where(p => p.ThuongHieu != null && p.ThuongHieu.Slug == brands);
            }

            // Lọc theo giá
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Gia >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Gia <= maxPrice.Value);
            }

            // Lọc theo thuộc tính
            if (attributeValues != null && attributeValues.Any())
            {
                products = products.Where(p => p.SanPhamThuocTinhs.Any(pt => attributeValues.Contains(pt.MaGiaTri)));
            }

            // Lấy tổng số sản phẩm sau khi áp dụng các bộ lọc
            var totalProducts = await products.Where(p => p.DaXoa == 0 && p.TrangThai == 1).CountAsync();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            // Lấy các sản phẩm cho trang hiện tại, sắp xếp theo NgayTao giảm dần
            var result = await products
                .Where(p => p.DaXoa == 0 && p.TrangThai == 1) // Chỉ lấy sản phẩm hiển thị và không bị xóa
                .Include(p => p.DanhGiaSanPhams)
                .OrderByDescending(p => p.NgayTao) // Sắp xếp theo ngày tạo giảm dần (mới nhất lên đầu)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductVM
                {
                    ProductId = p.MaSanPham,
                    ProductName = p.TenSanPham ?? "Chưa có tên",
                    Image = string.IsNullOrEmpty(p.HinhAnh) ? "/images/default-image.jpg" : p.HinhAnh,
                    Price = p.Gia,
                    PromotionPrice = p.GiaKhuyenMai,
                    DiscountPercent = p.PhanTramKhuyenMai,
                    ShortDescription = p.MoTaNgan ?? "Chưa có mô tả",
                    ProductRating = p.DanhGiaSanPhams.Any() ? p.DanhGiaSanPhams.Average(r => r.DiemDanhGia ?? 0) : 0,
                    Slug = p.Slug
                })
                .ToListAsync();

            // Lấy danh sách banner cho trang sản phẩm
            var banners = await _context.Sliders
                .Where(s => s.TrangThai == true && s.HienThiTrangSanPham == true)
                .OrderBy(s => s.ThuTuHienThi)
                .ToListAsync();
            ViewBag.Banners = banners;

            // Lấy danh sách thuộc tính và giá trị để hiển thị filter
            var attributes = await _context.ThuocTinhSanPhams
                .Where(a => a.HienThi)
                .Include(a => a.GiaTriThuocTinhs)
                .OrderBy(a => a.ThuTuHienThi)
                .ToListAsync();
            ViewBag.Attributes = attributes;

            var viewModel = new PagedProductListVM
            {
                SanPhams = result,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };

            return View(viewModel);
        }
        public async Task<IActionResult> SearchProduct(string? search = "", int page = 1)
        {
            var pageSize = 5;  // Số sản phẩm mỗi trang
            var products = _context.SanPhams.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower().Trim();
                products = products.Where(p =>
                    p.TenSanPham.ToLower().Contains(search) ||
                    p.MoTaNgan.ToLower().Contains(search));
            }
            var totalProducts = await products.CountAsync();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var result = await products
                .Where(p => p.TrangThai == 1)
                .Include(p => p.DanhGiaSanPhams)
                .OrderByDescending(p => p.NgayTao) // Sắp xếp theo ngày tạo giảm dần
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductVM
                {
                    ProductId = p.MaSanPham,
                    ProductName = p.TenSanPham!,
                    Image = p.HinhAnh ?? "",
                    Price = p.Gia,
                    PromotionPrice = p.GiaKhuyenMai,
                    DiscountPercent = p.PhanTramKhuyenMai,
                    ShortDescription = p.MoTaNgan!,
                    ProductRating = p.DanhGiaSanPhams.Any()
                        ? p.DanhGiaSanPhams.Average(r => (double)r.DiemDanhGia!) : 0,
                    TotalRating = p.DanhGiaSanPhams.Count,
                }).ToListAsync();
            var viewModel = new PagedProductListVM
            {
                SanPhams = result,  // Danh sách sản phẩm cho trang hiện tại
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };
            return View(viewModel);
        }
        [Route("ProductDetail/{slug}")]
        public async Task<IActionResult> ProductDetail(string? slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound();
            }
            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
            var customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : (int?)null;
            // Lấy sản phẩm, đánh giá, và bình luận từ cơ sở dữ liệu
            var product = await _context.SanPhams
                .Include(p => p.DanhMuc)
                .Include(p => p.ThuongHieu)
                .Include(p => p.HinhAnhSanPhams)
                .Include(p => p.BinhLuanSanPhams).ThenInclude(productComment => productComment.KhachHang)
                .Include(p => p.DanhGiaSanPhams)
                .ThenInclude(c => c.KhachHang)
                .FirstOrDefaultAsync(p => p.Slug == slug);

            if (product == null) // Kiểm tra sản phẩm tồn tại
            {
                return NotFound();
            }

            var relatedProducts = await _context.SanPhams
                .Where(p => p.MaDanhMuc == product.MaDanhMuc && p.MaThuongHieu == product.MaThuongHieu && p.MaSanPham != product.MaSanPham)
                .Take(5)
                .ToListAsync();
            // Tạo ViewModel
            var viewModel = new ProductDetailVM
            {

                SanPham = product,
                RelatedProducts = relatedProducts,
                ProductRating = product.DanhGiaSanPhams.Any()
                    ? product.DanhGiaSanPhams.Average(r => (double)r.DiemDanhGia!) // Tính trung bình điểm đánh giá
                    : 0,
                TotalRating = product.DanhGiaSanPhams.Count, // Tổng số đánh giá
                Comments = product.BinhLuanSanPhams
                    .Select(c => new ProductCommentVM
                    {
                        CustomerName = c.KhachHang?.TenHienThi ?? "Guest", // Hiển thị tên khách
                        Content = c.NoiDung,
                        CreatedAt = c.NgayTao,
                        Rating = product.DanhGiaSanPhams.FirstOrDefault(r => r.MaKhachHang == c.MaKhachHang)?.DiemDanhGia
                    }).ToList(),
            };

            return View(viewModel); // Trả về View
        }
        // Test action để kiểm tra sản phẩm trong database
        [HttpGet]
        [Route("TestProducts")]
        public async Task<IActionResult> TestProducts()
        {
            var products = await _context.SanPhams
                .Select(p => new { p.MaSanPham, p.TenSanPham, p.Slug })
                .ToListAsync();
            return Json(products);
        }

        [HttpPost]
        [Route("ProductDetail/{slug}/AddReview")]
        public async Task<IActionResult> AddReview(string slug, string content, int rating)
        {
            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
            int? customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : (int?)null;
            
            // Tìm sản phẩm theo slug
            var product = await _context.SanPhams
                .Where(p => p.Slug == slug && p.DaXoa == 0)
                .Select(p => new { p.MaSanPham, p.Slug })
                .FirstOrDefaultAsync();
                
            if (product == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index");
            }

            // Kiểm tra xem khách hàng đã đánh giá sản phẩm này chưa
            var existingRating = await _context.DanhGiaSanPhams
                .FirstOrDefaultAsync(r => r.MaSanPham == product.MaSanPham && r.MaKhachHang == customerId);

            if (existingRating != null)
            {
                // Cập nhật đánh giá cũ
                existingRating.DiemDanhGia = rating;
            }
            else
            {
                // Thêm đánh giá mới
                var productRating = new DanhGiaSanPham
                {
                    MaSanPham = product.MaSanPham,
                    MaKhachHang = customerId,
                    DiemDanhGia = rating
                };
                await _context.DanhGiaSanPhams.AddAsync(productRating);
            }

            // Thêm bình luận
            var comment = new BinhLuanSanPham
            {
                MaSanPham = product.MaSanPham,
                MaKhachHang = customerId,
                NoiDung = content,
                NgayTao = DateTime.Now
            };
            await _context.BinhLuanSanPhams.AddAsync(comment);

            try
            {
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đánh giá của bạn đã được gửi thành công!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Có lỗi xảy ra khi gửi đánh giá. Vui lòng thử lại sau.";
                // Log chi tiết lỗi để debug
                Console.WriteLine($"Error: {ex.Message}, MaSanPham: {product.MaSanPham}");
            }

            return RedirectToAction("ProductDetail", new { slug = product.Slug });
        }
        //public IActionResult AddToCart([FromBody] CartRequest request)
        //{
        //    if (request.Slug is null)
        //    {
        //        return BadRequest(new { message = "ID sản phẩm không hợp lệ!" });
        //    }

        //    var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "CustomerId");
        //    int? customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : (int?)null;

        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Json(new { success = false, message = "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng." });
        //    }

        //    // Tìm sản phẩm trong cơ sở dữ liệu
        //    var product = _context.Products.FirstOrDefault(p => p.Slug == request.Slug);
        //    if (product == null)
        //    {
        //        return BadRequest(new { message = "Sản phẩm không tồn tại!" });
        //    }

        //    // Tìm sản phẩm trong giỏ hàng của khách hàng
        //    var existingCartItem = _context.Carts.FirstOrDefault(c => c.ProductId == request.PrsluoductId && c.CustomerId == customerId);

        //    if (existingCartItem != null)
        //    {
        //        existingCartItem.Quantity++;
        //        if (existingCartItem.Quantity > product.Quantity)
        //        {
        //            existingCartItem.Quantity = product.Quantity ?? 0;
        //        }
        //        _context.Carts.Update(existingCartItem);
        //    }
        //    else
        //    {
        //        var newCartItem = new Cart
        //        {
        //            ProductId = request.ProductId,
        //            CustomerId = customerId,
        //            Quantity = 1,
        //            Price = (decimal?)product.Price
        //        };
        //        _context.Carts.Add(newCartItem);
        //    }

        //    _context.SaveChanges();
        //    return Ok(new { message = "Sản phẩm đã được thêm vào giỏ hàng!", success = true });
        //}
        //Thêm sản phẩm yêu thích
        [HttpPost]
        public IActionResult AddToWishlist(int productId)
        {
            var customerIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MaKhachHang");
            if (customerIdClaim == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập." });
            }

            var customerId = int.Parse(customerIdClaim.Value);
            var product = _context.SanPhams.Find(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });
            }

            var existingWishlist = _context.YeuThichs
                .FirstOrDefault(yt => yt.MaKhachHang == customerId && yt.MaSanPham == productId);

            if (existingWishlist == null)
            {
                var wishlistItem = new YeuThich
                {
                    MaKhachHang = customerId,
                    MaSanPham = productId
                };
                _context.YeuThichs.Add(wishlistItem);
                _context.SaveChanges();
                return Json(new { success = true, message = "Thêm vào yêu thích thành công.", isFavorite = true });
            }
            else
            {
                _context.YeuThichs.Remove(existingWishlist);
                _context.SaveChanges();
                return Json(new { success = true, message = "Xóa khỏi yêu thích thành công.", isFavorite = false });
            }
        }

    }
}