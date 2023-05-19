using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebSiteTest.Data;
 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var AspnetUserConnectionString = builder.Configuration.GetConnectionString("ComperWebSite");
builder.Services.AddDbContext<WebSiteTest.Areas.AspnetUser.Models.ComperWebSiteContext>(options =>
    options.UseSqlServer(AspnetUserConnectionString));
var ProductsFrontConnectionString = builder.Configuration.GetConnectionString("ComperWebSite");
builder.Services.AddDbContext<WebSiteTest.Areas.ProductsFront.Models.ComperWebSiteContext>(options =>
    options.UseSqlServer(ProductsFrontConnectionString));

//�s�u��.��ComperWebSite
var ComperWebSiteConnectionString = builder.Configuration.GetConnectionString("ComperWebSite");
builder.Services.AddDbContext<WebSiteTest.Areas.ThriftShop.Models.ComperWebSiteContext>(options =>
    options.UseSqlServer(ComperWebSiteConnectionString));
var ProductsManagerConnectionString = builder.Configuration.GetConnectionString("ComperWebSite");
builder.Services.AddDbContext<WebSiteTest.Areas.ProductsManager.Models.ComperWebSiteContext>(options =>
    options.UseSqlServer(ProductsManagerConnectionString));
var supplierConnectionString = builder.Configuration.GetConnectionString("ComperWebSite");
builder.Services.AddDbContext<WebSiteTest.Areas.supplier.Models.ComperWebSiteContext>(options =>
    options.UseSqlServer(supplierConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});
builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "ProductsFront",
      pattern: "{area:exists}/{controller=ProductsFront}/{action=showproduct}/{id?}"
    );
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "FrontThrift",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "ProductsManager",
      pattern: "{area:exists}/{controller=Products}/{action=Index}/{id?}"
    );
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "ThriftShop",
      pattern: "{area:exists}/{controller=ThriftProducts}/{action=Index}/{id?}"
    );
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "AspNetUser",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index_front}/{id?}");
app.MapRazorPages();


app.Run();
