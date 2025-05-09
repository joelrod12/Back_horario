using System.Text;
using Back_horario.Context;
using Back_horario.Models.Domain.Entities.Auth;
using Back_horario.Models.Domain.Entities.Email;
using Back_horario.Services.Interface;
using Back_horario.Services.Interface.Email;
using Back_horario.Services.Services;
using Back_horario.Services.Services.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Back_horario.Controllers;

var builder = WebApplication.CreateBuilder(args);


// Configuración CORS (agregar esto)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueFrontend",
        builder => builder
            .WithOrigins("http://localhost:5173") // Asegúrate que coincide con tu URL de Vue
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// inyeccion de dependencias 
builder.Services.AddTransient<IRolServices, RolServices>();
builder.Services.AddTransient<IUsuarioServices, UsuarioServices>();
builder.Services.AddTransient<IActividadServices, ActividadServices>();
builder.Services.AddTransient<IHorarioServices, HorarioServices>();
builder.Services.AddTransient<IGrupoServices, GrupoServices>();
builder.Services.AddTransient<ITemaServices, TemaServices>();
builder.Services.AddTransient<IMateriaServices, MateriaServices>();
builder.Services.AddTransient<IUsuario_MateriaServies, Usuario_MateriaServies>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailServices, EmailServices>();
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings")
);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Secret))
    throw new InvalidOperationException("La configuraci�n JWT no se carg� correctamente o la clave secreta est� vac�a.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    };
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://a    ka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowVueFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
