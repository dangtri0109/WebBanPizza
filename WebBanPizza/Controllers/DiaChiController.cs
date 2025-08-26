using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Session
using System.Linq;
using WebBanPizza.Models;

public class DiaChiController : Controller
{
    private readonly PizzaDbContext _ctx;
    public DiaChiController(PizzaDbContext ctx) => _ctx = ctx;

    private int? CurUserId() => HttpContext.Session.GetInt32("UserId");

    // =========================
    // DANH SÁCH + CHỌN ĐỊA CHỈ
    // =========================
    public IActionResult Index()
    {
        var uid = CurUserId();
        if (uid == null) return RedirectToAction("DangNhap", "Account");

        // Lấy từ DB trước (tránh lỗi translate khi OrderBy theo thuộc tính khó map)
        var list = _ctx.DiaChis
                       .Where(x => x.UserId == uid)
                       .ToList();

        // Sắp xếp trên RAM: địa chỉ mặc định lên đầu
        list = list
            .OrderByDescending(x => x.MacDinh == true)
            .ThenByDescending(x => x.DiaChiId)
            .ToList();

        return View(list); // Views/DiaChi/Index.cshtml
    }

    // =========================
    // TẠO MỚI (không render Tao.cshtml)
    // =========================
    [HttpGet]
    public IActionResult Tao()
    {
        if (CurUserId() == null) return RedirectToAction("DangNhap", "Account");
        // Không có view Tao.cshtml → quay lại Index
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Tao(DiaChi m)
    {
        var uid = CurUserId();
        if (uid == null) return RedirectToAction("DangNhap", "Account");

        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Vui lòng nhập đầy đủ thông tin địa chỉ hợp lệ.";
            return RedirectToAction(nameof(Index));
        }

        m.UserId = uid.Value;

        if (m.MacDinh)
        {
            var olds = _ctx.DiaChis.Where(x => x.UserId == uid && x.MacDinh);
            foreach (var x in olds) x.MacDinh = false;
        }

        _ctx.DiaChis.Add(m);
        _ctx.SaveChanges();
        TempData["Success"] = "Đã thêm địa chỉ giao hàng.";
        return RedirectToAction(nameof(Index));
    }

    // =========================
    // SỬA
    // =========================
    [HttpGet]
    public IActionResult Sua(int id)
    {
        var uid = CurUserId();
        if (uid == null) return RedirectToAction("DangNhap", "Account");

        var m = _ctx.DiaChis.FirstOrDefault(x => x.DiaChiId == id && x.UserId == uid);
        if (m == null) return NotFound();

        return View(m); // cần Views/DiaChi/Sua.cshtml
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Sua(DiaChi m)
    {
        var uid = CurUserId();
        if (uid == null) return RedirectToAction("DangNhap", "Account");

        var ent = _ctx.DiaChis.FirstOrDefault(x => x.DiaChiId == m.DiaChiId && x.UserId == uid);
        if (ent == null) return NotFound();

        ent.HoTen = m.HoTen;
        ent.SDT = m.SDT;
        ent.Duong = m.Duong;
        ent.QuanHuyen = m.QuanHuyen;
        ent.GhiChu = m.GhiChu;

        if (m.MacDinh && !ent.MacDinh)
        {
            var olds = _ctx.DiaChis.Where(x => x.UserId == uid && x.MacDinh);
            foreach (var x in olds) x.MacDinh = false;
            ent.MacDinh = true;
        }
        else if (!m.MacDinh)
        {
            ent.MacDinh = false;
        }

        _ctx.SaveChanges();
        TempData["Success"] = "Đã cập nhật địa chỉ.";
        return RedirectToAction(nameof(Index));
    }

    // =========================
    // XOÁ
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Xoa(int id)
    {
        var uid = CurUserId();
        if (uid == null) return RedirectToAction("DangNhap", "Account");

        var m = _ctx.DiaChis.FirstOrDefault(x => x.DiaChiId == id && x.UserId == uid);
        if (m != null)
        {
            _ctx.DiaChis.Remove(m);
            _ctx.SaveChanges();
            TempData["Success"] = "Đã xoá địa chỉ.";
        }
        return RedirectToAction(nameof(Index));
    }

    // =========================
    // ĐẶT MẶC ĐỊNH
    // =========================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DatMacDinh(int id)
    {
        var uid = CurUserId();
        if (uid == null) return RedirectToAction("DangNhap", "Account");

        var all = _ctx.DiaChis.Where(x => x.UserId == uid).ToList();
        foreach (var x in all) x.MacDinh = (x.DiaChiId == id);

        _ctx.SaveChanges();
        TempData["Success"] = "Đã đặt địa chỉ mặc định.";
        return RedirectToAction(nameof(Index));
    }
}
