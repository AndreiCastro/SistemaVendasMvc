using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    public class RelatorioModel
    {
        [DisplayName("Data De")]
        public DateTime DataDe { get; set; }

        [DisplayName("Data Até")]
        public DateTime DataAte { get; set; }
    }
}