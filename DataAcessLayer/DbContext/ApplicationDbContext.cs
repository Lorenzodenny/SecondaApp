using Microsoft.EntityFrameworkCore;
using SecondaApp.Model;

namespace SecondaApp.DataAcessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<Categoria> Categorie { get; set; }

        // Aggiungi una classe che mappa alla tua vista
        public DbSet<ProdottoCategoriaView> ProdottiCategorie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignora la migrazione automatica per la vista
            modelBuilder.Entity<ProdottoCategoriaView>().HasNoKey().ToView("ProdottoCategoriaView");


            // Imposta il valore predefinito per CategoriaId
            modelBuilder.Entity<Prodotto>()
                .Property(p => p.CategoriaId)
                .HasDefaultValue(1); // Sostituisci 1 con il valore di default desiderato
        }

    }
}
