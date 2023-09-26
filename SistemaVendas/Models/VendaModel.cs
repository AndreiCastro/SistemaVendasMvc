using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        [Key()]
        public int Id { get; set; }
        
        [Required]
        public DateTime Data { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [DisplayName("Quantidade do Produto")]
        public int Quantidade_Produto { get; set; }


        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public virtual ClienteModel Cliente { get; set; }

        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        public ProdutoModel Produto { get; set; }

        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public virtual VendedorModel Vendedor { get; set; }

    }
}
