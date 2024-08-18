using System.ComponentModel.DataAnnotations.Schema;

namespace SecondaApp.Model
{
    public class ProdottoCategoriaView
    {
        public int ProdottoCategoriaViewId { get; set; }
        public string ProdottoCategoriaViewNome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ProdottoCategoriaViewPrezzo { get; set; }
        public string CategoriaNome { get; set; }
    }

}
