using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBanPizza.Models;

public class PizzaController : Controller
{
    private readonly PizzaDbContext _context;

    public PizzaController(PizzaDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var pizzas = _context.Pizzas.ToList();
        return View(pizzas);
    }

    // ========== CHI TIẾT + ĐÁNH GIÁ ==========
    public IActionResult Details(int id)
    {
        var pizza = _context.Pizzas
            .Include(p => p.DanhGia)
            .ThenInclude(d => d.User) // để hiện tên người đánh giá
            .FirstOrDefault(p => p.PizzaId == id);

        if (pizza == null) return NotFound();

        // rating trung bình
        ViewBag.AvgRating = (pizza.DanhGia?.Any() ?? false)
            ? Math.Round(pizza.DanhGia.Where(x => x.SoSao.HasValue).Average(x => x.SoSao!.Value), 1)
            : 0;

        ViewBag.TotalRating = pizza.DanhGia?.Count() ?? 0;

        return View(pizza);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddReview(int pizzaId, int soSao, string? binhLuan)
    {
        // Lấy UserId từ Session (hoặc Identity của bạn)
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            TempData["Error"] = "Bạn cần đăng nhập để đánh giá.";
            return RedirectToAction(nameof(Details), new { id = pizzaId });
        }

        // (tuỳ chọn) chặn mỗi user chỉ đánh giá 1 lần / hoặc cho phép nhiều lần
        var review = new DanhGia
        {
            PizzaId = pizzaId,
            UserId = userId.Value,
            SoSao = Math.Clamp(soSao, 1, 5),
            BinhLuan = string.IsNullOrWhiteSpace(binhLuan) ? null : binhLuan.Trim(),
            NgayDanhGia = DateTime.Now
        };

        _context.DanhGia.Add(review);
        _context.SaveChanges();

        TempData["Success"] = "Cảm ơn bạn đã đánh giá!";
        return RedirectToAction(nameof(Details), new { id = pizzaId });
    }

    // ========== TÌM KIẾM & LỌC GIỮ NGUYÊN ==========
    [HttpGet]
    public IActionResult TimKiem(string keyword)
    {
        var q = (keyword ?? string.Empty).Trim();

        var kq = _context.Pizzas
            .Where(p => q == ""
                || EF.Functions.Like(p.Ten!, $"%{q}%")
                || EF.Functions.Like(p.MoTa!, $"%{q}%"))
            .OrderBy(p => p.Ten)
            .ToList();

        ViewBag.Keyword = q;
        return View(kq);
    }

    [HttpGet]
    public IActionResult Loc(string? keyword, int? danhMucId, string? kichThuoc, decimal? giaMin, decimal? giaMax)
    {
        var q = _context.Pizzas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
            q = q.Where(p => p.Ten!.Contains(keyword));

        if (danhMucId.HasValue)
            q = q.Where(p => p.DanhMucId == danhMucId);

        if (!string.IsNullOrEmpty(kichThuoc))
            q = q.Where(p => p.KichThuoc == kichThuoc);

        if (giaMin.HasValue)
            q = q.Where(p => p.Gia >= giaMin.Value);

        if (giaMax.HasValue)
            q = q.Where(p => p.Gia <= giaMax.Value);

        var vm = new PizzaFilterVM
        {
            Keyword = keyword,
            DanhMucId = danhMucId,
            KichThuoc = kichThuoc,
            GiaMin = giaMin,
            GiaMax = giaMax,
            KetQua = q.OrderBy(p => p.Ten).ToList(),
            DanhMucs = _context.DanhMucs.ToList()
        };

        return View(vm);
    }
}
