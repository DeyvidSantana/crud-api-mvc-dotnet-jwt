namespace CrudWebDotnet.Models.Usuario
{
    public class LoginViewModelOutput
    {
        public string Token { get; set; }
        public LoginViewModelDetailsOutput Usuario { get; set; }
    }

    public class LoginViewModelDetailsOutput
    {
        public int Codigo { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
