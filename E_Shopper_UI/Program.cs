using E_Shopper_Business.Abstract;
using E_Shopper_Business.Concrete;
using E_Shopper_DataAccess.Abstract;
using E_Shopper_DataAccess.Concrete.EfCore;
using E_Shopper_UI.EmailServices;
using E_Shopper_UI.Identity;
using E_Shopper_UI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    //password
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    //user
    //options.User.AllowedUserNameCharacters = ""; //Username de "" kullanma
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = true; // Confirmed : Email onaylans�n
    options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon numaras� onaylanamas� zorunlu de�ildir.

});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/Account/accessdenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = "EShopper.Security.Cookie",
        SameSite = SameSiteMode.Strict  //Bu Cookie sadece bizim taray�c�dan server taraf�na ta��n�r.
    };

});

//AddScoped: Gelen her bir web request i�in bir instance olu�turur ve gelen her ayn� request te ayn� instance'� kullan�l�r,farkl� web requestleri i�inde yeniden instance al�r.
builder.Services.AddScoped<IProductDal, EfCoreProductDal>();
builder.Services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

builder.Services.AddTransient<IEmailSender,EmailSender>();

//MVC Mimarisini Tan�mlad�m.
//builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    app.UseExceptionHandler("/Error");
}
else
{
    SeedDatabase.Seed();
}
app.UseStaticFiles();
app.CustomStaticFiles(); //middleWare: Bootstrap npm ile indilecek ve node module i�erisindeki static dosyalar� ile d��ar�ya a�ma i�lemi
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


/*
  routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
 */
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "products",
        pattern: "products/{category?}",
        defaults: new { controller = "Shop", action = "List" });

    endpoints.MapControllerRoute(
       name: "adminProducts",
       pattern: "admin/products",
       defaults: new { controller = "Admin", action = "ProductList" });

    endpoints.MapControllerRoute(
      name: "adminProducts",
      pattern: "admin/products/{id}",
      defaults: new { controller = "Admin", action = "EditProduct" });
});
app.Run();
