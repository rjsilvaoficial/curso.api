using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Usuarios
{
    public class RegistrarViewModelInput
    {
        [Required(ErrorMessage = "Campo Login é obrigatório!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo Email é obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Senha é obrigatório!")]
        public string Senha { get; set; }
    }
}
