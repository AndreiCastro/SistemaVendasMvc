using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace SistemaVendas.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "{0} deve conter de {2} a {1} caracteres.")]
        public string Nome{ get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória.")]
        [DisplayName("Descrição")]
        [MinLength(3, ErrorMessage = "Descrição deve conter no mínino {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório.")]
        [DisplayName("Preço")]
        public decimal Preco_Unitario { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [DisplayName("Quantidade")]
        public decimal Quantidade_Estoque { get; set; }

        [Required(ErrorMessage = "Unidade é obrigatória.")]
        [DisplayName("Unidade Medida")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "{0} deve conter de {2} a {1} caracteres.")]
        public string Unidade_Medida { get; set; }

        [Required(ErrorMessage = "Foto é obrigatória.")]
        [DisplayName("Link Foto")]
        public string Link_Foto { get; set; }
    }
}
