using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVendas.Models
{
    public class ClienteModel
    {
        [Key()]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("CpfCnpj")]
        public string CpfCnpj { get; set; }
                
        [Column("Email")]
        public string Email { get; set; }
                
        [Column("Senha")]
        public string Senha { get; set; }
    }
}
