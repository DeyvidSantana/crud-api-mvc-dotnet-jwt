using System.ComponentModel.DataAnnotations;

namespace CrudWebDotnet.Models.Usuario
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "O login é obrigatório!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória!")]
        public string Senha { get; set; }
    }
}
