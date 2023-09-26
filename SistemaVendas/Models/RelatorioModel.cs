using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVendas.Models
{
    public class RelatorioModel
    {
        [NotMapped]
        [DisplayName("Data De")]
        public DateTime DataDe { get; set; }

        [NotMapped]
        [DisplayName("Data Até")]
        public DateTime DataAte { get; set; }
    }
}