using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        public int Id { get; set; }
        
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória.")]
        [DisplayName("Quantidade Produto")]
        public int Quantidade_Produto { get; set; }

        [Required]
        public int IdVendedor { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [Required]
        public int IdProduto { get; set; }


        [NotMapped]
        public string NomeCliente { get; set; }

        [NotMapped]
        public string NomeVendedor { get; set; }

        [NotMapped]
        public string NomeProduto { get; set; }
    }
}
