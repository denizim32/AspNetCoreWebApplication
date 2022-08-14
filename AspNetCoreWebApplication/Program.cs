using AspNetCoreWebApplication.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Entity framework
builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlServer()); // o => o. türündeki kod yazým tarzýna "lambda expression" denir.

// Oturum açma
builder.Services.AddAuthentication().AddCookie(o =>
{
    o.LoginPath = "/Admin/Login";
    o.Cookie.Name = "AdminLogin"; // Cookinin ismi AdminLogin olsun
});
// Projeye admin paneli eklemek için Projeye sað týk açýlan menüden Add > New scaffolded item diyerek açýlan pencereden Area yý seçip Admin ismini verip ok diyerek alaný oluþturuyoruz.

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

// Oturum açmayý aktif et
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute( // Bu alaný area kýsmýnda admin area yý kullanabilmek için ekledik. Eðer baþka arealar eklersek yine bu alana eklemeliyiz.
            name: "admin",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
