using Microsoft.EntityFrameworkCore;
using WebBanPizza.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Bắt buộc cho Session
builder.Services.AddDistributedMemoryCache();

// ✅ Đăng ký Session (có thể cấu hình thêm)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // thời gian sống
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // cần thiết để hoạt động khi có GDPR
});

// DbContext
builder.Services.AddDbContext<PizzaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaDbContext")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ⬇️ ⬇️ Đặt Session SAU UseRouting và TRƯỚC Authorization/Map
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
