namespace SecondaApp.Model
{
    public class UpdateProfileModel
    {
        public string UserId { get; set; }  // Assicurati di passare l'ID dell'utente
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

}
