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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field. Example: 'Bearer your_token_here'",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


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

// Registro il servizio per le email
builder.Services.AddTransient<IEmailSender, EmailSender>();


// Registro UnitOfWork interfaccia per transizioni
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configurazione dei ruoli di default
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    var adminEmail = "admin@yourapp.com";
    var adminPassword = "Admin123!";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Nome = "AdminNome", // Aggiungi un nome predefinito
            Cognome = "AdminCognome", // Aggiungi un cognome predefinito
            NumeroDiTelefono = "3334747806"
        };
        var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
        if (createAdmin.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

}

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
