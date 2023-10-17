using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SistemaVendas.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve conter de {2} a {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CPF/CNPJ é obrigatório.")]
        [DisplayName("CPF/CNPJ")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF/CNPJ deve conter de {2} a {1} caracteres.")]
        public string CpfCnpj { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail com formato inválido.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "E-mail deve conter de {2} a {1} caracteres.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} deve conter {2} a {1} caracteres.")]
        public string Senha { get; set; }

        [NotMapped]
        [Compare("Senha", ErrorMessage = "Senha informada é diferente do campo Senha")]
        [DisplayName("Confirmar Senha")]
        public string ComparaSenha { get; set; }
    }
}
