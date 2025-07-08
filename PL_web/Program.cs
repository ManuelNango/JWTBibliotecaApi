using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//2. (Program.cs PL) Agregar la cadena de conexión, que previmente colocamos en el JSON
builder.Services.AddDbContext<JwtexamenBibliotecaContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("JWTExamenBiblioteca")));

//3. Apuntamos a la interfaz y a la clase de BL. (paso 4 en Interfaz BL)
builder.Services.AddScoped<BL.IUsuario, BL.Usuario>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
