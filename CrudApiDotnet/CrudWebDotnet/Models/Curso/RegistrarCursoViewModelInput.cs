using System.ComponentModel.DataAnnotations;

namespace CrudWebDotnet.Models.Curso
{
    public class RegistrarCursoViewModelInput
    {
        [Required(ErrorMessage = "O nome é obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória!")]
        public string Descricao { get; set; }
    }
}
