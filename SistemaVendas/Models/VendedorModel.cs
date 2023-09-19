using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SistemaVendas.Models
{
    public class VendedorModel
    {

        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} deve conter de {2} a {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail com formato inválido.")]        
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} deve conter de {2} a {1} caracteres.")]
        public string Senha { get; set; }

        [NotMapped]
        [Compare("Senha", ErrorMessage = "Senha informada é diferente do campo Senha")]
        public string ComparaSenha { get; set; }
    }
}
