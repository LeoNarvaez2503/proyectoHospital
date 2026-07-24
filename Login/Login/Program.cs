using CapaDatos;
using Login.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "UsuarioLogin";
        config.LoginPath = "/Acceso/Login";
        config.AccessDeniedPath = "/Acceso/Denegado";
    });

var app = builder.Build();

var cadenaDAL = new CadenaDAL();
DatabaseInitializer.Initialize(cadenaDAL.cadenaDato);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
