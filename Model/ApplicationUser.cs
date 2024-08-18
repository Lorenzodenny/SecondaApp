using Microsoft.AspNetCore.Identity;

namespace SecondaApp.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string NumeroDiTelefono { get; set; }
    }
}
