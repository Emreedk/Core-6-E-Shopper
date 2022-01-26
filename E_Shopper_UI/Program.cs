using E_Shopper_Business.Abstract;
using E_Shopper_Business.Concrete;
using E_Shopper_DataAccess.Abstract;
using E_Shopper_DataAccess.Concrete.EfCore;
using E_Shopper_UI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//AddScoped: Gelen her bir web request i�in bir instance olu�turur ve gelen her ayn� request te ayn� instance'� kullan�l�r,farkl� web requestleri i�inde yeniden instance al�r.
builder.Services.AddScoped<IProductDal, EfCoreProductDal>();
builder.Services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();

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
