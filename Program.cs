using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecondaApp.Abstract;
using SecondaApp.BusinessLayer.Service;
using SecondaApp.DataAcessLayer;
using SecondaApp.DataAcessLayer.Repository;
using SecondaApp.Model;
using SecondaApp.Service;
using SecondaApp.Utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurazione del DbContext con Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurazione di Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Aggiungi l'autenticazione JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Aggiungi supporto per le Razor Pages (necessario per le pagine di Identity)
builder.Services.AddRazorPages();

// Registro Service e Repository generici
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));

// Registro Service per prodotto con Stored Procedure
builder.Services.AddScoped<IProdottoRepository, ProdottoRepository>();
builder.Services.AddScoped<IProdottoService, ProdottoService>();

// Registro Service e Repo per StoredProcedure per le Categorie
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

// Registro la vista ProdottoCategoriaView
builder.Services.AddScoped<IProdottoCategoriaViewRepository, ProdottoCategoriaViewRepository>();
builder.Services.AddScoped<IProdottoCategoriaViewService, ProdottoCategoriaViewService>();


// Registro UnitOfWork interfaccia per transizioni
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication(); // Aggiungi l'autenticazione
app.UseAuthorization();

app.MapRazorPages(); // Mappa le Razor Pages
app.MapControllers();

app.Run();
