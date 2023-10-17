using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVendas.Dtos
{
    public class VendaDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [DisplayName("Quantidade do Produto")]
        public int QuantidadeProduto { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public virtual ClienteDto Cliente { get; set; }
        
        [ForeignKey("Produto")]
        public int IdProduto { get; set; }
        public virtual ProdutoDto Produto { get; set; }
        
        [ForeignKey("Vendedor")]
        public int IdVendedor { get; set; }
        public virtual VendedorDto Vendedor { get; set; }
    }
}
