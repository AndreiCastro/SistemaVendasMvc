using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SistemaVendas.Models
{
    public class ProdutoModel
    {
        [Key()]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} deve conter de {2} a {1} caracteres.")]
        public string Nome{ get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória.")]        
        [MinLength(3, ErrorMessage = "Descrição deve conter no mínino {1} caracteres.")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório.")]
        [DisplayName("Preço")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [DisplayName("Quantidade")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "Unidade de Medida é obrigatória.")]        
        [StringLength(3, MinimumLength = 1, ErrorMessage = "{0} deve conter de {2} a {1} caracteres.")]
        [DisplayName("Unidade Medida")]
        public string UnidadeMedida { get; set; }

        [DisplayName("Link Foto")]
        public string Link_Foto { get; set; }
    }
}
