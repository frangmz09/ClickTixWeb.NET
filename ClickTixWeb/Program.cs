using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ClickTixWeb.Models;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using FirebaseAdmin;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ClicktixContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("conexion"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.24-mariadb")));

// Agrega la configuración de caché en memoria y de sesión.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

string basePath = AppDomain.CurrentDomain.BaseDirectory; // o Environment.CurrentDirectory
string jsonFilePath = Path.Combine(basePath, "Credential", "clicktixmobile-firebase-adminsdk-vl0f0-453b69dcdc.json");


// Inicializar Firebase
FirebaseApp.Create(new AppOptions
{

    Credential = GoogleCredential.FromFile(jsonFilePath),
});




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
