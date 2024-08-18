using System.ComponentModel.DataAnnotations.Schema;

namespace SecondaApp.Model
{
    public class Prodotto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Prezzo { get; set; }

        public string Descrizione { get; set; }

        // Aggiungi la relazione con Categoria
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
