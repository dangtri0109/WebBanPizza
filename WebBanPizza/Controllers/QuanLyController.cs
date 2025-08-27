using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using WebBanPizza.Models;

namespace WebBanPizza.Controllers
{
    public class QuanLyController : Controller
    {
        private readonly PizzaDbContext _context;
        private readonly IWebHostEnvironment _env;

        public QuanLyController(PizzaDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("VaiTro");
            return role != null && role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
        }

        // ======================== QUẢN LÝ ĐƠN HÀNG ========================
        [HttpGet]
        public async Task<IActionResult> DonHang()
        {
            if (!IsAdmin()) return Unauthorized();

            var donHangs = await _context.DonHangs
                .Include(d => d.User)
                .OrderByDescending(d => d.NgayDat)
                .ToListAsync();

            return View(donHangs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThai, bool daThanhToan, string returnUrl)
        {
            if (!IsAdmin()) return Unauthorized();

            var donHang = await _context.DonHangs.FirstOrDefaultAsync(x => x.DonHangId == id);
            if (donHang == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction(nameof(DonHang));
            }

            donHang.TrangThai = trangThai;
            donHang.DaThanhToan = daThanhToan || string.Equals(trangThai, "Hoàn tất", StringComparison.OrdinalIgnoreCase);

            _context.LichSuTrangThaiDonHangs.Add(new LichSuTrangThaiDonHang
            {
                DonHangId = donHang.DonHangId,
                TrangThai = donHang.TrangThai,
                ThoiGian = DateTime.Now
            });

            await _context.SaveChangesAsync();
            TempData["Success"] = "✅ Đã cập nhật đơn #" + donHang.DonHangId + " ➜ " + donHang.TrangThai + ".";

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrWhiteSpace(referer))
            {
                try
                {
                    var uri = new Uri(referer);
                    var local = uri.PathAndQuery;
                    if (Url.IsLocalUrl(local)) return Redirect(local);
                }
                catch { }
            }
            return RedirectToAction(nameof(DonHang));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GiaoHangThanhCong(int id, string returnUrl)
        {
            if (!IsAdmin()) return Unauthorized();

            var don = await _context.DonHangs.FirstOrDefaultAsync(d => d.DonHangId == id);
            if (don != null && string.Equals(don.TrangThai, "Đang giao", StringComparison.OrdinalIgnoreCase))
            {
                don.TrangThai = "Hoàn tất";
                don.DaThanhToan = true;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đơn hàng đã được giao thành công!";
            }

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrWhiteSpace(referer))
            {
                try
                {
                    var uri = new Uri(referer);
                    var local = uri.PathAndQuery;
                    if (Url.IsLocalUrl(local)) return Redirect(local);
                }
                catch { }
            }
            return RedirectToAction(nameof(DonHang));
        }

        // ======================== QUẢN LÝ PIZZA ========================
        [HttpGet]
        public async Task<IActionResult> Pizza()
        {
            if (!IsAdmin()) return Unauthorized();
            var list = await _context.Pizzas
                .Include(p => p.DanhMuc)
                .OrderByDescending(p => p.PizzaId)
                .ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult ThemPizza()
        {
            if (!IsAdmin()) return Unauthorized();
            ViewBag.DanhMucs = new SelectList(
                _context.DanhMucs.OrderBy(x => x.TenDanhMuc).ToList(),
                "DanhMucId", "TenDanhMuc"
            );
            return View(new PizzaUploadViewModel { KichThuoc = "M" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemPizza(PizzaUploadViewModel model)
        {
            if (!IsAdmin()) return Unauthorized();

            if (model.DanhMucId <= 0)
                ModelState.AddModelError("DanhMucId", "Danh mục là bắt buộc.");

            if (string.IsNullOrWhiteSpace(model.KichThuoc) ||
                (model.KichThuoc != "S" && model.KichThuoc != "M" && model.KichThuoc != "L"))
            {
                ModelState.AddModelError("KichThuoc", "Kích thước phải là S, M hoặc L.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.DanhMucs = new SelectList(
                    _context.DanhMucs.OrderBy(x => x.TenDanhMuc).ToList(),
                    "DanhMucId", "TenDanhMuc"
                );
                return View(model);
            }

            var fileName = await SaveImageAsync(model.HinhAnhUpload, model.HinhAnhCu);

            var pizza = new Pizza
            {
                Ten = model.Ten,
                MoTa = model.MoTa,
                Gia = model.Gia,
                DanhMucId = model.DanhMucId,
                KichThuoc = model.KichThuoc,
                HinhAnh = string.IsNullOrEmpty(fileName) ? "default.jpg" : fileName
            };

            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
            TempData["Success"] = "✅ Thêm pizza thành công!";
            return RedirectToAction(nameof(Pizza));
        }

        [HttpGet]
        public async Task<IActionResult> SuaPizza(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null) return NotFound();

            var vm = new PizzaUploadViewModel
            {
                PizzaId = pizza.PizzaId,
                Ten = pizza.Ten,
                MoTa = pizza.MoTa ?? string.Empty,
                Gia = pizza.Gia,
                DanhMucId = pizza.DanhMucId ?? 0,
                KichThuoc = string.IsNullOrEmpty(pizza.KichThuoc) ? "M" : pizza.KichThuoc,
                HinhAnhCu = pizza.HinhAnh ?? string.Empty
            };

            ViewBag.DanhMucs = new SelectList(
                _context.DanhMucs.OrderBy(x => x.TenDanhMuc).ToList(),
                "DanhMucId", "TenDanhMuc",
                vm.DanhMucId
            );
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaPizza(PizzaUploadViewModel model)
        {
            if (!IsAdmin()) return Unauthorized();

            var pizza = await _context.Pizzas.FindAsync(model.PizzaId);
            if (pizza == null) return NotFound();

            if (model.DanhMucId <= 0)
                ModelState.AddModelError("DanhMucId", "Danh mục là bắt buộc.");
            if (string.IsNullOrWhiteSpace(model.KichThuoc) ||
                (model.KichThuoc != "S" && model.KichThuoc != "M" && model.KichThuoc != "L"))
            {
                ModelState.AddModelError("KichThuoc", "Kích thước phải là S, M hoặc L.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.DanhMucs = new SelectList(
                    _context.DanhMucs.OrderBy(x => x.TenDanhMuc).ToList(),
                    "DanhMucId", "TenDanhMuc",
                    model.DanhMucId
                );
                return View(model);
            }

            var fileName = await SaveImageAsync(model.HinhAnhUpload, model.HinhAnhCu);

            pizza.Ten = model.Ten;
            pizza.MoTa = model.MoTa;
            pizza.Gia = model.Gia;
            pizza.DanhMucId = model.DanhMucId;
            pizza.KichThuoc = model.KichThuoc;
            if (!string.IsNullOrEmpty(fileName))
                pizza.HinhAnh = fileName;

            await _context.SaveChangesAsync();
            TempData["Success"] = "✅ Cập nhật pizza thành công!";
            return RedirectToAction(nameof(Pizza));
        }

        // GET để tránh 405 khi lỡ truy cập URL trực tiếp
        [HttpGet]
        public async Task<IActionResult> XoaPizza(int id)
        {
            if (!IsAdmin()) return Unauthorized();

            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza != null)
            {
                if (!string.IsNullOrEmpty(pizza.HinhAnh) && pizza.HinhAnh != "default.jpg")
                {
                    var path = Path.Combine(_env.WebRootPath, "images", pizza.HinhAnh);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                }
                _context.Pizzas.Remove(pizza);
                await _context.SaveChangesAsync();
                TempData["Success"] = "🗑️ Xóa pizza thành công!";
            }
            return RedirectToAction("Pizza", "QuanLy");
        }

        // ======================== DASHBOARD ========================
        [HttpGet]
        public async Task<IActionResult> Dashboard(int? thang, int? nam)
        {
            if (!IsAdmin()) return Unauthorized();

            var donHangs = _context.DonHangs.AsQueryable();

            if (thang.HasValue)
                donHangs = donHangs.Where(d => d.NgayDat.HasValue && d.NgayDat.Value.Month == thang.Value);

            if (nam.HasValue)
                donHangs = donHangs.Where(d => d.NgayDat.HasValue && d.NgayDat.Value.Year == nam.Value);

            var list = await donHangs.Where(d => d.NgayDat.HasValue).ToListAsync();

            ViewBag.TongDoanhThu = list.Sum(d => d.TongTien);
            ViewBag.SoDon = list.Count;
            ViewBag.ChoXuLy = list.Count(d => d.TrangThai == "Chờ xử lý");
            ViewBag.DangGiao = list.Count(d => d.TrangThai == "Đang giao");
            ViewBag.HoanTat = list.Count(d => d.TrangThai == "Hoàn tất");

            var doanhThuTheoNgay = list
                .GroupBy(d => d.NgayDat.Value.Date)
                .Select(g => new { Ngay = g.Key.ToString("dd/MM/yyyy"), Tong = g.Sum(x => x.TongTien) })
                .OrderBy(x => DateTime.ParseExact(x.Ngay, "dd/MM/yyyy", null))
                .ToList();

            ViewBag.DoanhThuChart = System.Text.Json.JsonSerializer.Serialize(doanhThuTheoNgay);
            return View();
        }

        // ======================== COUPON ========================
        [HttpGet]
        public async Task<IActionResult> Coupon()
        {
            if (!IsAdmin()) return Unauthorized();
            var list = await _context.Coupons.OrderByDescending(c => c.CouponId).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult TaoCoupon()
        {
            if (!IsAdmin()) return Unauthorized();
            return View(new Coupon { NgayHetHan = DateTime.Now.AddMonths(1) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaoCoupon(Coupon c)
        {
            if (!IsAdmin()) return Unauthorized();

            if (string.IsNullOrWhiteSpace(c.Ma))
                ModelState.AddModelError(nameof(c.Ma), "Nhập mã.");
            if (!c.GiamGiaPhanTram.HasValue || c.GiamGiaPhanTram < 1 || c.GiamGiaPhanTram > 100)
                ModelState.AddModelError(nameof(c.GiamGiaPhanTram), "Phần trăm 1–100.");
            if (!ModelState.IsValid) return View(c);

            if (await _context.Coupons.AnyAsync(x => x.Ma == c.Ma))
            {
                ModelState.AddModelError(nameof(c.Ma), "Mã đã tồn tại.");
                return View(c);
            }

            _context.Coupons.Add(c);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã tạo mã giảm giá.";
            return RedirectToAction(nameof(Coupon));
        }

        [HttpGet]
        public async Task<IActionResult> SuaCoupon(int id)
        {
            if (!IsAdmin()) return Unauthorized();
            var c = await _context.Coupons.FindAsync(id);
            if (c == null) return NotFound();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaCoupon(Coupon c)
        {
            if (!IsAdmin()) return Unauthorized();

            if (string.IsNullOrWhiteSpace(c.Ma))
                ModelState.AddModelError(nameof(c.Ma), "Nhập mã.");
            if (!c.GiamGiaPhanTram.HasValue || c.GiamGiaPhanTram < 1 || c.GiamGiaPhanTram > 100)
                ModelState.AddModelError(nameof(c.GiamGiaPhanTram), "Phần trăm 1–100.");
            if (!ModelState.IsValid) return View(c);

            if (await _context.Coupons.AnyAsync(x => x.Ma == c.Ma && x.CouponId != c.CouponId))
            {
                ModelState.AddModelError(nameof(c.Ma), "Mã đã tồn tại.");
                return View(c);
            }

            _context.Coupons.Update(c);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật mã.";
            return RedirectToAction(nameof(Coupon));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> XoaCoupon(int id)
        {
            if (!IsAdmin()) return Unauthorized();
            var c = await _context.Coupons.FindAsync(id);
            if (c != null)
            {
                _context.Coupons.Remove(c);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã xoá mã.";
            }
            return RedirectToAction(nameof(Coupon));
        }

        // ======================== LỊCH SỬ ĐƠN HÀNG USER ========================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var uid = HttpContext.Session.GetInt32("UserId");
            if (!uid.HasValue) return RedirectToAction("DangNhap", "Account");

            var donHangs = await _context.DonHangs
                .Include(d => d.User)
                .Include(d => d.DiaChi)
                .Where(d => d.UserId == uid.Value)
                .OrderByDescending(d => d.NgayDat)
                .ToListAsync();

            return View(donHangs);
        }

        // ======================== HELPER LƯU ẢNH ========================
        private async Task<string> SaveImageAsync(IFormFile file, string oldName)
        {
            if (file == null || file.Length == 0) return oldName;

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            if (!allowed.Contains(ext))
            {
                ModelState.AddModelError("HinhAnhUpload", "Chỉ cho phép ảnh JPG, PNG, GIF, WEBP.");
                return oldName;
            }
            if (file.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("HinhAnhUpload", "Kích thước ảnh tối đa là 2MB.");
                return oldName;
            }
            if (!ModelState.IsValid) return oldName;

            if (!string.IsNullOrEmpty(oldName) && oldName != "default.jpg")
            {
                var oldPath = Path.Combine(_env.WebRootPath, "images", oldName);
                if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
            }

            var newName = Guid.NewGuid().ToString("N") + ext;
            var dir = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var savePath = Path.Combine(dir, newName);
            using (var stream = System.IO.File.Create(savePath))
            {
                await file.CopyToAsync(stream);
            }
            return newName;
        }
    }
}
