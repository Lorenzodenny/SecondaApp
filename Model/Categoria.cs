using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SecondaApp.Model
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }

        // Relazione con Prodotto
        // [ValidateNever] => per non attivre mai la validazione
        public ICollection<Prodotto>? Prodotti { get; set; }
    }
}
