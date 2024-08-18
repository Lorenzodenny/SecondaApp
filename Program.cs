using Microsoft.EntityFrameworkCore;
using SecondaApp.Abstract;
using SecondaApp.BusinessLayer.Service;
using SecondaApp.DataAcessLayer;
using SecondaApp.DataAcessLayer.Repository;
using SecondaApp.Service;
using SecondaApp.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro la connessione al DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
