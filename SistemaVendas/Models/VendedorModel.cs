using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SistemaVendas.Models
{
    public class VendedorModel
    {
        [Key()]
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Email")]        
        public string Email { get; set; }
                
        [Column("Senha")]
        public string Senha { get; set; }                
    }
}
