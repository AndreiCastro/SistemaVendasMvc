using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Campo {0} deve ter de {2} a {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ é obrigatório.")]
        [DisplayName("CPF/CNPJ")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "Campo CPF/CNPJ deve entre {2} a {1} caracteres.")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail com formato inválido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Campo {0} deve entre {2} a {1} caracteres.")]
        public string Senha { get; set; }
    }
}
