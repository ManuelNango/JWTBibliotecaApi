using DL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Custom;

var builder = WebApplication.CreateBuilder(args);

//2. (Program.cs PL) Agregar la cadena de conexión, que previmente colocamos en el JSON
builder.Services.AddDbContext<JwtexamenBibliotecaContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("JWTExamenBiblioteca")));

//3. Apuntamos a la interfaz y a la clase de BL
builder.Services.AddScoped<BL.IMedio, BL.Medio>();

builder.Services.AddScoped<BL.IAutor, BL.Autor>();

builder.Services.AddScoped<BL.IUsuario, BL.Usuario>();

//Lo de CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy("MiPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5158")

        .AllowAnyMethod()

        .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Hacer referencia a la BD
builder.Services.AddDbContext<DL.JwtexamenBibliotecaContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("JWTExamenBiblioteca")));

//Utilizar clase utilidades (para encriptar contraseña y generar token)
builder.Services.AddSingleton<Utilidades>();

//Agregar autenticación
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config => 
{
    config.RequireHttpsMetadata = false; //para multimedia
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false, //Si queremos validar aplicación por la URL
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//PIPELINE

//Habilitar CORS
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("MiPolicy"); //Habilita CORS

//Decirle a la app que utilice Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();  //Mapea endpoints de API

app.Run();
