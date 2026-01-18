using E_learninig.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// my part aa
builder.Services.AddMemoryCache();

builder.Services.AddSession();


// Add services to the container.


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EleariningContext>(options =>


options.UseSqlServer(

builder.Configuration.GetConnectionString("ElearningContextStr")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


// my part aa
app.UseSession();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();