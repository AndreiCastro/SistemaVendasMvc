using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace SistemaVendas.Models
{
    public class ProdutoModel
    {
        [Key()]
        [Column("Id")]
        public int Id { get; set; }
                
        [Column("Nome")]
        public string Nome{ get; set; }
                
        [Column("Descricao")]
        public string Descricao { get; set; }
                
        [Column("PrecoUnitario")]
        public decimal PrecoUnitario { get; set; }
                
        [Column("QuantidadeEstoque")]
        public int QuantidadeEstoque { get; set; }
                
        [Column("UnidadeMedida")]
        public string UnidadeMedida { get; set; }
                
        [Column("Link_foto")]
        public string Link_Foto { get; set; }
    }
}
