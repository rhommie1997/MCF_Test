using MCF_TestWeb.Services.Interface;
using MCF_TestWeb.Services.Services;
using MCF_TestWeb.Util;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
    ).AddCookie(option =>
    {
        option.LoginPath = "/Login/Index";
        option.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ILoginService, LoginService>();
builder.Services.AddHttpClient<IBPKBService, BPKBService>();
builder.Services.AddHttpClient<ILocationService, LocationService>();


SD.MCF_TestAPIBase = builder.Configuration["ServiceUrls:MCF_TestAPI"];

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBPKBService, BPKBService>();
builder.Services.AddScoped<ILocationService, LocationService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
