using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    public class LoginModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail inserido é inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }
    }
}
