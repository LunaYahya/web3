using E_learninig.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EleariningContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ElearningContextStr")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// عرض الأخطاء التفصيلية - غير هذا الجزء
app.UseDeveloperExceptionPage(); // أضف هذا السطر لعرض تفاصيل الأخطاء

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();